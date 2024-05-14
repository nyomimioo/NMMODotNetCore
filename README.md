https://www.entityframeworktutorial.net/efcore/create-model-for-existing-database-in-ef-core.aspx
<efcore command database first>
Scaffold-DbContext "Server=DELL\SQLEXPRESS;Database=DotNetTrainingBath4;User ID=sa;Password=sa@123;TrustServerCertificate=true;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context DotNetTrainingBath4Context 
Scaffold-DbContext [-Connection] [-Provider] [-OutputDir] [-Context] [-Schemas>] [-Tables>] 
                    [-DataAnnotations] [-Force] [-Project] [-StartupProject] [<CommonParameters>]
   //to create Models folder

=<>10.5.2024 API and EFCore
API                                         
HTTP method                     
get=>read
post=>create
put/patch=>update
delete=>delete
http status code 
Informational responses (100 – 199)
Successful responses (200 – 299)
Redirection messages (300 – 399)
Client error responses (400 – 499)
Server error responses (500 – 599)
12.5.2025 RestApi that connected with EFCore











