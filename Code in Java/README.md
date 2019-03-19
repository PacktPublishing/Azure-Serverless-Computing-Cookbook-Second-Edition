# Azure Serverless Computing Cookbook Second Edition Java
## Introduction
When using **Java** with **Azure Functions** we don't have the portal support available with **C#**.  Because of this we will need to do the development in **Visual Studio Code** right from the start.  If you have followed along in the **C#** view presented in the mainline of the book, this will be unfamiliar until Chapter 4.
## Getting Started
### Preparing your development workstation
To follow along with the recipes in this book you will need to setup a development workstation and install the following software:
- JDK (version 8 or above)
- Maven
- NodeJS (required for the package manager - npm)
- Azure Functions Core Tools (npm install -g azure-functions-core-tools)
- Visual Studio Code
- Azure Storage Explorer

You will also need to install the following extensions for Visual Studio Code:
- Java Extension Pack
- Debugger for Java
- C#
- Azure Functions
### Your first project
Once all the above is installed you can start your first Azure Functions as follows:
1. In Visual Studio Code, open the Azure Functions explorer
2. Create a new function by clicking on the **Create functions..** icon
3. Click **Browse..**
4. Create a folder to hold your function project and select it
5. When the initialisation dialog pops up click **Yes**
6. Select **Java** as the language for your function project
7. Enter a **Group ID** for the maven project. I used **com.packtpub**
8. Enter an **Artefact ID** for the maven project.  I used **packtpub-functions**
9. Enter a **Version** for the maven project.  I used **1.0-SNAPSHOT**
10. Enter a **Java Package** for the maven project.  I used **com.packtpub**
11. Enter a **Name** for the function.  Based on the first Recipe in **Chapter 1**, I used **RegisterUser**
12. Select **HttpTrigger**
13. Enter a **Java Package** again.  I used **com.packtpub**
14. Enter a **Function Name**.  I used **Register User**
15. Select the **Anonymous** Authorisation Level
16. Select how you would like to open your project.  I used **Open in current window**
17.  Your function project will now initialise and open in the explorer.  All the required configuration files are automatically created.  In addition to your **RegisterUser.java**, a default called **Function.java** will also have been created; you are free to delete that source file and any test source files related to it.

### Deploying your first project
1. In the Azure Functions explorer in Visual Studio Code, click the "**Deploy to Function App…**" icon (The first time you do this you will need to login to Azure)
2. Select the Azure subscription you wish to deploy to
3. Click "**Create New Function App**" or select an existing Azure Function App (if selecting an existing app, you can ignore the following steps)
4. Enter a globally unique name for your function app
5. Click "**Create new resource group**"
6. Enter a name for your resource group
7. Click "**Create new storage account**"
8. Enter a name for your storage account
9. Select a region to deploy into

### Things to check
If you get a class not found exeception when trying to run your new Azure functions then make sure the "**FUNCTIONS_WORKER_RUNTIME**" setting is set to "**java**"

## So, what now?
At this point you will have a completely configured project folder and have deployed it to a new or existing Azure Function Application.  Key files in that structure are:
- **extensions.csproj** Configuration for installing the NuGet packages needed to support bindings that are not core to Azure Functions
- **host.json** Configuration settings for Azure Functions.  Most settings are configured with Annotations when using Java so we generally leave this unchanged
- **pom.xml** The Maven build and deployment configuration.  We will modify this with dependencies as needed

The Chapter directories you will find here contain Recipe folders which are complete project folders and each recipe builds on the previous one, so the above steps are for your information and to help you if you want to start with a fresh project and build up your own functions independently of the recipes.
