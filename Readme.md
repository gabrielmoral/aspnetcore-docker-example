### An example with docker containers, docker-compose and azure container instances

As a part of a learning process with **docker** and **azure** I've created an example with those technologies.

The app is a simple API with two storage mechanisms redis and sql.

Two dockerfiles to setup the API and the database images.

Three docker compose files to:
    * Execute the app. 
    * Run unit tests in the CD pipeline.
    * Run end to end tests in the CD pipeline.

Template file to define the deployment in azure container instances.

Script to deploy the containers in azure. (It assumes the images are available in a public repository, in this case mine).