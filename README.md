# RetailManager
A retail management system
<br /><br/>

## Login Page
User can log in using their credentials. Login and password are being sent to the webAPI. 
<br />WebAPI authenticates the user and give them JWT token (Resource Owner Password Flow). 
<br />After successful login user is redirected to Sales Page.
<br /><br/>
![login](https://user-images.githubusercontent.com/33805319/134479675-4fd1a860-2110-46e2-ba32-bfe83f7ccef1.png)
<br /><br/>

## Sales Page
User is able to select items from the list and add them to the Cart with appropriate quantity. If wrong items were added, user can remove them using Remove from Cart button.
<br />When item is being added to the Cart the Subtotal, Tax, Total are being automatically calculated. 
<br />Check Out button send data to the webAPI, where they are saved to Database using Dapper.
<br /><br/>
![sales](https://user-images.githubusercontent.com/33805319/134479826-46f68686-2177-4ee7-b92a-b044664712a1.png)
<br /><br/>

## User Administration Page
Only users with Admin role can access this page. Admin can add/remove role to/from the selected user.
<br /><br/>
![userAdministration](https://user-images.githubusercontent.com/33805319/134479869-43d3bc8d-d9c5-4715-bc40-dca511ad4b13.png)
<br /><br/>

## Used Technologies
- ASP .NET Framework 4.7
- ASP .NET Core 3.1 / 2.1 (App was updated from .net framework to .net core)
- RESTful API
- ASP .NET CORE MVC
- MVVM Calibrun.Micro
- Json Web Token
- Dapper 
- AutoMapper, Swagger
- Github


<br />
<br />
Link to the course: https://youtube.com/playlist?list=PLLWMQd6PeGY0bEMxObA6dtYXuJOGfxSPx
