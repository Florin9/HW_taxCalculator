# HW_taxCalculator
To run this app, you'll need a connection to a PostgreSQL database. To create a containerized PostgreSQL database you can use the following command, replacing [User] and [Pass] with your own settings:
```
docker run -e POSTGRES_USER=[User] -e POSTGRES_PASSWORD=[Pass] -p 5432:5432 -d postgres
```
Once you have that setup, you can build the app using
```
docker build -t taxcalculator -f Dockerfile .
```
Finally, you can run the app with
```
docker run -p 80:80 -e ConnectionStrings:Persistence="Host=[IpOfPostgreSQL];Port=5432;User ID=[User];Password=[Pass];Database=TaxCalculator;Pooling=true;" taxcalculator
```
Replace [IpOfPostgreSQL] with the ip of the docker container:
```
docker inspect -f '{{range.NetworkSettings.Networks}}{{.IPAddress}}{{end}}' containerId
```
Afterwards, go to http://localhost:80/swagger and you'll see the endpoints
