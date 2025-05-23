# Steeltoe Sample Applications

This repository contains a variety of sample applications illustrating how to use the Steeltoe framework:

## [Configuration](Configuration)

Samples using the Spring Cloud Config Server and other Steeltoe configuration providers.

| Sample | Status |
| --- | --- |
| [ConfigurationProviders](Configuration/src/ConfigurationProviders) | [![Build Status](https://dev.azure.com/SteeltoeOSS/Steeltoe/_apis/build/status%2FSamples%2FSteeltoe-Samples-Configuration-ConfigurationProviders?branchName=main)](https://dev.azure.com/SteeltoeOSS/Steeltoe/_build/latest?definitionId=73&branchName=main) |

## [Discovery](Discovery)

Samples using Steeltoe Service Discovery microservices-based applications.

## [Management](Management/src)

Samples using the Steeltoe Management packages for adding Management REST endpoints to your application, as well as adding Distributed Tracing support.

| Sample | Status |
| --- | --- |
| [ActuatorWeb](./Management/src/ActuatorWeb/) | [![Build Status](https://dev.azure.com/SteeltoeOSS/Steeltoe/_apis/build/status%2FSamples%2FSteeltoeOSS.Samples%20%5BManagement_CloudFoundry%5D?branchName=main)](https://dev.azure.com/SteeltoeOSS/Steeltoe/_build/latest?definitionId=23&branchName=main) |

## [Connectors](Connectors)

Samples using the Steeltoe Connectors for connecting to backing services. Steeltoe Connectors simplify the coding process of binding to and accessing Cloud Foundry-based services.

| Sample | Status |
| --- | --- |
| [CosmosDb](Connectors/src/CosmosDb) | [![Build Status](https://dev.azure.com/SteeltoeOSS/Steeltoe/_apis/build/status%2FSamples%2FSteeltoe-Samples-Connectors-CosmosDb?branchName=main)](https://dev.azure.com/SteeltoeOSS/Steeltoe/_build/latest?definitionId=69&branchName=main) |
| [MongoDb](Connectors/src/MongoDb) | [![Build Status](https://dev.azure.com/SteeltoeOSS/Steeltoe/_apis/build/status%2FSamples%2FSteeltoe-Samples-Connectors-MongoDb?branchName=main)](https://dev.azure.com/SteeltoeOSS/Steeltoe/_build/latest?definitionId=70&branchName=main) |
| [MySql](Connectors/src/MySql) | [![Build Status](https://dev.azure.com/SteeltoeOSS/Steeltoe/_apis/build/status%2FSamples%2FSteeltoe-Samples-Connectors-MySql?branchName=main)](https://dev.azure.com/SteeltoeOSS/Steeltoe/_build/latest?definitionId=17&branchName=main) |
| [MySqlEFCore](Connectors/src/MySqlEFCore) | [![Build Status](https://dev.azure.com/SteeltoeOSS/Steeltoe/_apis/build/status%2FSamples%2FSteeltoe-Samples-Connectors-MySqlEFCore?branchName=main)](https://dev.azure.com/SteeltoeOSS/Steeltoe/_build/latest?definitionId=18&branchName=main) |
| [PostgreSql](Connectors/src/PostgreSql) | [![Build Status](https://dev.azure.com/SteeltoeOSS/Steeltoe/_apis/build/status%2FSamples%2FSteeltoe-Samples-Connectors-PostgreSql?branchName=main)](https://dev.azure.com/SteeltoeOSS/Steeltoe/_build/latest?definitionId=21&branchName=main) |
| [PostgreSqlEFCore](Connectors/src/PostgreSqlEFCore) | [![Build Status](https://dev.azure.com/SteeltoeOSS/Steeltoe/_apis/build/status%2FSamples%2FSteeltoe-Samples-Connectors-PostgreSqlEFCore?branchName=main)](https://dev.azure.com/SteeltoeOSS/Steeltoe/_build/latest?definitionId=22&branchName=main) |
| [RabbitMQ](Connectors/src/RabbitMQ) | [![Build Status](https://dev.azure.com/SteeltoeOSS/Steeltoe/_apis/build/status%2FSamples%2FSteeltoe-Samples-Connectors-RabbitMQ?branchName=main)](https://dev.azure.com/SteeltoeOSS/Steeltoe/_build/latest?definitionId=14&branchName=main) |
| [Redis](Connectors/src/Redis) | [![Build Status](https://dev.azure.com/SteeltoeOSS/Steeltoe/_apis/build/status%2FSamples%2FSteeltoe-Samples-Connectors-Redis?branchName=main)](https://dev.azure.com/SteeltoeOSS/Steeltoe/_build/latest?definitionId=20&branchName=main) |
| [SqlServerEFCore](Connectors/src/SqlServerEFCore) | [![Build Status](https://dev.azure.com/SteeltoeOSS/Steeltoe/_apis/build/status%2FSamples%2FSteeltoe-Samples-Connectors-SqlServerEFCore?branchName=main)](https://dev.azure.com/SteeltoeOSS/Steeltoe/_build/latest?definitionId=71&branchName=main) |

## [Network File Shares](FileShares)

Sample using Steeltoe to manage the connection with a Windows (SMB) network file share.

| Sample | Status |
| --- | --- |
| [FileSharesWeb](FileShares/src/FileSharesWeb/) | [![Build Status](https://dev.azure.com/SteeltoeOSS/Steeltoe/_apis/build/status%2FSamples%2FSteeltoe-Samples-FileShares-FileSharesWeb?branchName=main)](https://dev.azure.com/SteeltoeOSS/Steeltoe/_build/latest?definitionId=76&branchName=main) |

## [Security](Security)

Samples using the Steeltoe Security packages for Authentication and Authorization with Cloud Foundry auth services and using a Redis cache for DataProtection KeyRing storage.

| Sample | Status |
| --- | --- |
| [RedisDataProtection](Security/src/RedisDataProtection) | [![Build Status](https://dev.azure.com/SteeltoeOSS/Steeltoe/_apis/build/status%2FSamples%2FSteeltoe-Samples-Security-RedisDataProtection?branchName=main)](https://dev.azure.com/SteeltoeOSS/Steeltoe/_build/latest?definitionId=74&branchName=main) |

# Branches

All new development is done on the main branch. Samples for released Steeltoe versions can be found in their respective branches. For example, branch "3.x" contains samples for the latest Steeltoe 3.x release.

# Documentation

If you are looking for documentation on how to use the Steeltoe components, you can find that [here](https://steeltoe.io/docs/).

# Building & Running

See the Readmes for each sample for instructions on how to build and run.
