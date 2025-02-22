# Book Management API
## Overview
The repository provides a Book Management API that is built with ASP.NET Core and EF Core. I have attempted to separate the data retrieval logic from the business- and application-related concerns, that is why there are services in the DAL project. Even though the services somehow remind the repository, their usage might be justified by the concern for future extension and introduction of additional business- or application-related operations. Some validation was implemented on the level of models, so that the user should not be able to submit incorrect dates. Only the books with unique titles can be added - this  was set up on the database level using EF Core entity configuration. The "popularity score" of the books is calculated by a domain service implementing the corresponding interface. The domain Book model does not leak into the presentation layer thanks to the usage of DTOs. The soft delete is implemented by adding an additional "IsDeleted" property and using EF Core's query filters.
