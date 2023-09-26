const { ipcRenderer } = require("electron");

const executeButton = document.getElementById("executeButton");
const output = document.getElementById("output");
const commandForm = document.getElementById("commandForm");

executeButton.addEventListener("click", () => {
  executeButton.disabled = true;
  loader.style.display = "flex";
  const subject = document.getElementById("subject").value;
  const companyName = document.getElementById("companyName").value;
  const numberOfItems = document.getElementById("numberOfItems").value;

  const command = `mickey -n "${companyName}" -s "${subject}" -c ${numberOfItems}`;

  ipcRenderer.send("execute-command", command);
});

ipcRenderer.on("command-result", (event, result) => {
  const data = JSON.parse(result);
  executeButton.disabled = false;

  let html = "";

  data.forEach((item) => {
    html += `
            <div class="tweet">
                <div class="tweet-header">
                <div class="event-info">
                <span class="event-name">${item.Event}</span>
                <span class="event-date">${item.Date}</span>
                </div>
                </div>
                ${
                  item.Image && item.Image !== ""
                    ? `<img class="tweet-image" src="${item.Image}" />`
                    : ""
                }
                <div class="tweet-content">${item.Tweet.replaceAll(
                  "\n",
                  ""
                )}</div>
                <button class="copy-button">Copier</button>
            </div>
        `;
  });

  // Remplacez le contenu de la sortie avec le HTML généré.
  output.innerHTML = html;
  loader.style.display = "none";
  const copyButtons = document.querySelectorAll(".copy-button");
  copyButtons.forEach((button, index) => {
    button.addEventListener("click", () => {
      const tweetContent = data[index].Tweet;
      navigator.clipboard.writeText(tweetContent);
    });
  });
});
