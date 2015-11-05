# SSQ signon examples 
Example application stacks that use the [SSQ signon](https://www.ssqsignon.com) online authorization server.

## Example stacks include

- A [Node.js](https://nodejs.org) server, [angular.js](https://angularjs.org/) client stack.
- An [ASP.Net](http://www.asp.net/) server,  [angular.js](https://angularjs.org/) client stack.

## Each example stack includes

- A "basic" web app with username/password authentication.
- A "slave" web app that authenticates with the basic web app through *single sign on*.
- An example *http users endpoint* through which *SSQ signon* can communicate with your user database.
 

## Node.js - Angular.js setup

### 1. Register with *SSQ singon* and create a *module*.
 
1.1 - In *step 1* of the module creation wizard, add `http://localhost:9901` and `http://localhost:9902` to the list of *Allowed origins*.

1.2 - In *step 2* of the module creation wizard, select the `dummy endpoint` and some three dummy user accounts.
At least some of the accounts should contain the scopes `cat` and `dog`, which will be used in the example. 

1.3 - In *step 3* of the module creation wizard, select the `password` authorization type for your first client, and check
the `use dummy user endpoint` checkbox. 

### 2. Checkout the repository.

### 3. In the command prompt, navigate to the `/nodejs-angularjs` directory, and run `npm install`.

### 4. Basic webapp

4.1 Rename the `/nodejs-angularjs/basic-webapp/config.example.js` file to `/nodejs-angularjs/basic-webapp/config.js`.

4.2 Open the `/nodejs-angularjs/basic-webapp/config.js` file and change the `moduleName` value to the name of the module you've created in step 1.

4.3 Rename the `/nodejs-angularjs/basic-webapp/client/config.example.js` file to `/nodejs-angularjs/basic-webapp/client/config.js`.

4.4 Open the `/nodejs-angularjs/basic-webapp/client/config.js` file and change the `SSQSIGNON_MODULE_NAME` value to the name of the module you've created in step 1.
Also, change the `SSQSIGNON_CLIENT_ID` value to the *client id* of the `password` authorization type client you've created in step 1 (you may
check this value in your module's *clients* section).

4.6 In the command prompt, navigate to `/nodejs-angularjs/basic-webapp` and run `node server.js`. If all goes well you should see the
`web app listening on port 9901` message.

4.7 Open your web browser and navigate to `http://localhost:9901`. You may now login with the dummy user accounts you've created in step 1.
    Users that have the `cat` scope will be able to see a picture of a cat after logging in.
    Users that have the `dog` scope will be able to see a picture of a dog after logging in.

## Developed using:

- [Node.js](https://nodejs.org) (server side) 
- [ASP.Net](http://www.asp.net/) (server side)
- [angular.js](https://angularjs.org/) (client side)



