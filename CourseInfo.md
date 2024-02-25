https://learning.oreilly.com/videos/net-core-microservices/9781803247793/9781803247793-video3_1/



> cd C:/development/swagger-codegen

> Invoke-WebRequest -OutFile swagger-codegen-cli.jar https://repo1.maven.org/maven2/io/swagger/codegen/v3/swagger-codegen-cli/3.0.52/swagger-codegen-cli-3.0.52.jar

> java -jar swagger-codegen-cli.jar generate -i http://localhost:5243/swagger/v1/swagger.json -l csharp -o C:/Development/CodeGround/BookStore/BookStore.Web/BookStore.Web/ApiClients