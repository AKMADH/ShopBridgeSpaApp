# README #

This README would normally document whatever steps are necessary to get your application up and running.

### What is this repository for? ###

* Quick summary
* Version asp.net core web api 3.1
* [Learn Markdown](https://bitbucket.org/tutorials/markdowndemo)

### How do I get set up? ###

* just clone the application and Rebuild the solution file and there are 3 folder when you will see the solution Explorer 
  1.Infrastrcture :-The folder contains the information about the Database configuration and 
  inside the configfile folder  shopbridge.json file is there which contains information about database connectivity.
  2.Inventory:- This folder contains the information regarding Api controller method and data fecthing from database and 
   crud operations.
   ClientApp :- contains front end code which is releated to angular from there the api call is going .
    just run the command npm install
    1.Install Firebase npm install @angular/fire 
    3.install sweetalert npm install --save sweetalert2


    For Updating the Api end point Update the Api url in the environment.prod.ts file inside environment folder
   3.UnitTestCase:-This folder contains the information regarding Unit Test Cases
  
* Configuration
* Dependencies
* Database configuration
  first create the database and stored there is one file storedprocedure.sql inside the  DataBaseFile  folder which comes 
  under infrastructure directory.
   change the servername, password ,userid according to you and update the shopbridge.json file inside the configfile 
   folder which comes under infrastructure directory. 
. 
* How to run the project
  first go to the path Inventory->src->ShopBridge.Modules.Inventory.Api project select this as a startup project and Rebuild the solution once and press F5.


### Contribution guidelines ###

* Writing tests
* Code review
* Other guidelines

### Who do I talk to? ###

* Repo owner or admin
* Other community or team contact
