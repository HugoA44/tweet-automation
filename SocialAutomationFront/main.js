const { app, BrowserWindow, ipcMain } = require("electron");
const { exec } = require("child_process");

let mainWindow;

app.on("ready", () => {
  mainWindow = new BrowserWindow({
    width: 800,
    height: 600,
    webPreferences: {
      nodeIntegration: true,
      contextIsolation: false,
    },
  });

  mainWindow.loadFile("index.html");

  ipcMain.on("execute-command", (event, command) => {
    exec(command, (error, stdout, stderr) => {
      if (error) {
        event.sender.send("command-result", `Erreur : ${error.message}`);
        return;
      }
      if (stderr) {
        event.sender.send(
          "command-result",
          `Erreur de la commande : ${stderr}`
        );
        return;
      }
      event.sender.send("command-result", stdout);
    });
  });
});
