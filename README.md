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

1.2 - In *step 2* of the module creation wizard, select the `dummy endpoint` and some dummy user accounts.
At least some of the accounts should contain the scopes `cat`, `dog` and `hamster`, which will be used in the example.
For example, the following dummy user accounts may be created:

- username: `test`, password: `testtest`, scope: `cat`
- username: `test2`, password: `testtest2`, scope: `cat dog hamster`
- username: `test3`, password: `testtest3`, scope: `dog hamster`

1.3 - In *step 3* of the module creation wizard, select the `password` authorization type for your first client, and check
the `use dummy user endpoint` and `generate refresh tokens` checkboxes. 

### 2. Checkout the repository.

### 3. Npm install
In the command prompt, navigate to the `/nodejs-angularjs` directory, and run `npm install`.

### 4. Basic web app

4.1 Rename the `/nodejs-angularjs/basic-webapp/config.example.js` file to `/nodejs-angularjs/basic-webapp/config.js`.

4.2 Open the `/nodejs-angularjs/basic-webapp/config.js` file and change the `moduleName` value to the name of the module you've created in step 1.

4.3 Rename the `/nodejs-angularjs/basic-webapp/client/config.example.js` file to `/nodejs-angularjs/basic-webapp/client/config.js`.

4.4 Open the `/nodejs-angularjs/basic-webapp/client/config.js` file and change the `SSQSIGNON_MODULE_NAME` value to the name of the module you've created in step 1.
Also, change the `SSQSIGNON_CLIENT_ID` value to the *client id* of the client you've registered in step 1 (you may
check this value in your module's *clients* section).

4.6 In the command prompt, navigate to `/nodejs-angularjs/basic-webapp` and run `node server.js`. If all goes well you should see the
`web app listening on port 9901` message.

4.7 Open your web browser and navigate to `http://localhost:9901`. You may now login with the dummy user accounts you've created in step 1.
    Users that have the `cat` scope will be able to see a picture of a cat after logging in.
    Users that have the `dog` scope will be able to see a picture of a dog after logging in.
    
### 5. SSO slave web app

5.1 The *basic web app* that you set up in step 4 should already be up and running. 

5.2 In the [SSQ signon module admin](https://ssqsignon.com/moduleadmin), register a new client with the *authorization
 type* set to `Authorization code`, the *use dummy user endpoint* and *generate refresh tokens* checkboxes checked,
 and `http://localhost:9902` added to the list of *valid redirect URIs*. 

5.3 Rename the `/nodejs-angularjs/sso-slave-webapp/config.example.js` file to `/nodejs-angularjs/sso-slave-webapp/config.js`.

5.4 Open the `/nodejs-angularjs/sso-slave-webapp/config.js` file and change the `moduleName` value to the name of the module you've created in step 1.
  Also change the `clientId` and `clientSecret` to the *client id* and *client secret* of client you've registered in step 5.2.
  
5.5 Rename the `/nodejs-angularjs/sso-slave-webapp/client/config.example.js` file to `/nodejs-angularjs/sso-slave-webapp/client/config.js`.

5.6 Open the `/nodejs-angularjs/sso-slave-webapp/client/config.js` file and change the `SSQSIGNON_MODULE_NAME` value to the name of the module you've created in step 1.
Also, change the `SSQSIGNON_CLIENT_ID` value to the *client id* of client you've registered in step 5.2.

5.7 In the command prompt, navigate to `/nodejs-angularjs/sso-slave-webapp` and run `node server.js`. If all goes well you should see the
`web app listening on port 9902` message.

5.8 Open your web browser and navigate to `http://localhost:9902`. Clicking the *login with SSQ singon example app* button will redirect you to the *basic web app*
  for Single Sign on. Once logged in, users that have the `hamster` scope will be able to see a picture of a hamster.
  
### 6. The http users endpoint

6.1 Deploy the node.js server found in `/nodejs-angularjs/http-users-endpoint/server.js` on your favourite hosting service. You may want to edit to `port`
    variable to 80.

6.2 Go to the *Settings* section of your module in the [SSQ signon module admin](https://ssqsignon.com/moduleadmin) and
Scroll down to the *HTTP users endpoint* panel. Check the enabled checkbox, set both the *Authenticate and authorize URI* and
*Reauthorize URI* to the `/users` path on your deployed server, e.g. `http://path-to-your-deployed-server-js/users`. Leave the
*Authentication type* as `Basic`, set the *Username* to `example` and the password to `testtest`.

6.3 **Save changes** to your module settings.

6.4 Go to the *Clients* section of your module in the [SSQ signon module admin](https://ssqsignon.com/moduleadmin),
edit both previously created clients and uncheck the *use dummy user endpoint* checkbox.

6.5 You should now be able to log in to *basic web app* and *SSO slave web app*  using the 2 accounts hardcoded into `server.js`:
`test1@users.com` (password: `testtest1`) and `test2@users.com` (password: `testtest2`).

## Developed using:

- [Node.js](https://nodejs.org) (server side) 
- [ASP.Net](http://www.asp.net/) (server side)
- [angular.js](https://angularjs.org/) (client side)



