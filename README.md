# Calculator

Sample web app for a calculator demo. Consisting of an [Angular 15](https://github.com/angular/angular) front end and [.NET 7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) api.

## Prerequisites

* .NET7 - Download the latest .NET [here](https://dotnet.microsoft.com/en-us/download/dotnet/7.0).
* Node.JS ^14.20.0 - Down NVM [here](https://github.com/coreybutler/nvm-windows) and install with `nvm install 14.20.0`.

## Development server

Run `npm install` to install dependencie then `ng serve` on /angularapp and `dotnet run`  on /WebApi for a dev server. Navigate to `http://localhost:4200/`.
The app will automatically reload if you change any of the source files.
Note: If running from Visual Studio, there can be a bug preventing the WebApi from starting, regardless if it is selected as the first startup project, either run one project from the console or another VS instance.
## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` on /angularapp to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build. Run `dotnet build` on /WebApi.

## Running unit tests

Run `ng test` on /angularapp to execute the web unit tests via [Karma](https://karma-runner.github.io).
Run `dotnet test` on /WebApi to execute the api tests.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).
