# Show the directory structure of the API and React app ignoring unnecesary files from dotnet and react

#!/bin/bash
tree -I 'bin|obj|node_modules|.git|.vscode|.idea|.DS_Store' 
