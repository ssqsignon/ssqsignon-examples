# SSQ signon .Net - ASP.Net - Angular.js example setup

## 1. Register with *SSQ singon* and create an *Authorization server*.

Remember to add some dummy user accounts. Some of the accounts should contain the scopes
`cat`, `dog` and `hamster`, which will be used in the example.
For example, the following dummy user accounts may be created:

- username: `test`, password: `testtest`, scope: `cat`
- username: `test2`, password: `testtest2`, scope: `cat dog hamster`
- username: `test3`, password: `testtest3`, scope: `dog hamster`

An initial *Master app* will be registered for you. Please check the App's id in the *Apps* section of the admin panel.

## 2. Checkout the repository.

`https://github.com/ssqsignon/ssqsignon-examples`

## 3. Build the solution
Open `/dotnet-aspnet-angularjs/aspnet-angularjs.sln` with Visual Studio (2013+). The NuGet packages should be downloaded automatically.

## 4. Basic web app

4.1 Rename the `/dotnet-aspnet-angularjs/basic-webapp/SSQSignon.example.config` file to `/dotnet-aspnet-angularjs/basic-webapp/SSQSignon.config`.

4.2 Open the `/dotnet-aspnet-angularjs/basic-webapp/SSQSignon.config` file and 

- change the `SSQSignonServerName` value to the name of your *Authorization server*.
- change the `SSQSignonAppId` value to the id of your registered *Master app*.

4.3 Run the `basic-webapp` project. If all goes well your browser will be directed to `http://localhost:9901`
and you will see the login dialog.

4.4 You may now login with the dummy user accounts you've created in step 1.
    Users that have the `cat` scope will be able to see a picture of a cat after logging in.
    Users that have the `dog` scope will be able to see a picture of a dog after logging in.
    
## 5. SSO slave web app

5.1 The *Basic web app* that you set up in step 4 should already be up and running. 

5.2 In the [SSQ signon module admin](https://ssqsignon.com/moduleadmin), register a new *Slave app*,
leave the *use dummy user endpoint* and *generate refresh tokens* checkboxes checked,
and add `http://localhost:9902` to the list of *valid redirect URIs*. 

5.3 Rename the `/dotnet-aspnet-angularjs/sso-slave-webapp/SSQSignon.example.config` file to
`/dotnet-aspnet-angularjs/sso-slave-webapp/SSQSignon.config`.

5.4 Open the `/dotnet-aspnet-angularjs/sso-slave-webapp/SSQSignon.config` file and change the
`SSQSignonServerName` value to the name of your *Authorization server*.
Also change the `SSQSignonAppId` and `SSQSignonAppSecret` to the *App id* and *App secret* of your *Slave app*.

5.5 Run the `sso-slave-webapp` project. If all goes well your browser will be directed to
`http://localhost:9902` and you will see the app page.

5.6 Clicking the *login with SSQ singon example app* button will redirect you to *basic web app*
for Single Sign on. Once logged in, users that have the `hamster` scope will be able to see a picture of a hamster.
  
## 6. The http users endpoint

6.1 Publish the web server found in the `http-users-endpoint` to your favourite hosting service.

6.2 Go to the *Settings* section of your *Authorization server* in [SSQ signon admin](https://ssqsignon.com/moduleadmin) and
Scroll down to the *HTTP users endpoint* panel. Check the enabled checkbox, set both the *Authenticate and authorize URI* and
*Reauthorize URI* to the `/users` path on your deployed server, e.g. `http://path-to-your-deployed-server-js/users`. Leave the
*Authentication type* as `Basic`, set the *Username* to `example` and the password to `testtest`.

6.3 **Save changes** to your authorization server settings.

6.4 Go to the *Apps* section of your *Authorization server* in the [SSQ signon admin](https://ssqsignon.com/moduleadmin),
edit both previously created *Apps* and uncheck the *use dummy user endpoint* checkbox.

6.5 You should now be able to log in to *basic web app* and *SSO slave web app*  using the 2 accounts hardcoded into `UsersController`:
`test1@users.com` (password: `testtest1`) and `test2@users.com` (password: `testtest2`).
