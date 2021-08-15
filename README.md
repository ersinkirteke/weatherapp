1-) run docker command with cmd

docker-compose --project-name appsfactory up

sql server login : server: localhost,1453 user: sa password: appsfactory2021!

2-)run appsfactory.weather.api project with visual studio

3-) make a request with swagger or you can use postman collection json (Appsfactory.postman_collection.json) inside repository for all the requests

4-) open appsfactory.weather.ui folder with visual studio code

5-)run following commands below with visual code terminal

	npm install
	npx vue-cli-service build --mode development
	npx vue-cli-service serve --mode development
	

USED PATTERNS and PRINCIPLES:

	Unit of Work pattern
	Generic Repository pattern
	Specifiation pattern
	Service layer
	DDD(Domain Driven Design)
	MVC pattern
	SOLID principle
	Common Reuse principle
	Common Closure principle
	DRY
	YAGNI
	KISS
    
USED PACKAGES:

	Swashbuckle for open api specification
	FluenValidation for validations
	Entity Framework Core
	Moq for unit tests
  
TODOs:
	
	use mediatR and Refit for CQRS pattern
	use Cassandra for audit logging
	use enterpise messaging architecture
	write unit test for controllers and services
	ui tests with jest or enzyme
	make ui more responsive with bootstrap 4 and meterial design
	add elk stack for logging
	use correlationid for tracking
	use key vault and key valet pattern for appsettings credentials
	dockerize all the project
	use kubernetes for orchestration
