Welcome to EuroQuest! 

This is our group projekt on a traveling agency where you can book a trip to some selected countries from our database.
There are many activities, hotels and cities to cose from. 

To get started you have to make sure that you have Thunderclient or Postman downloaded.
(This is to actualy use the program)

Make sure to also have MySql downloaded. 
(This is for the databse)

Last thing is to have visuel studio code downloaded
(This is to see the actual code and open the files for the database)

Okey you have everything downloaded so lets get started. 


==============================================================================================================================
1 
Go to the repository EuroQuest and press the green button that says (<>Code)
cose either HTTPS or SSH and copie the url to clipboard. 
Now open your terminal preferibley: git bash. Navigate to your wished directory and type: (git clone <url>)
Now open the projekt in VS code by Typeing: (code .)

2 
If step one is done correctly you will now have visuel studio opend and the project in front of you. 
Open the folder Database and select the Schema.sql and copy everything to clipboard.
Start the MySql workbench and paste everything from the clipboard then run it.
Now select the Data.sql and do the same for this file. 

3
Now open your terminal and type: (dotnet run).
You will now have a line that says: (Now listening on: http://localhost:XXXX).
Now you can open the thundeclient or postman and type: (http://localhost:<your four digits>) 
This is where you use the program and if you open the program.cs in Visuel studio you will se a sorted list with endpoints.
They look like this: (app.MapGet("/users", Users.Get);)
To call an endpoint you simply take the string "/users" and paste it next to the url like this: (http://localhost:XXXX/users)
It is important to check if the endpoint is a Get, Post or Delete. change the request accordingly. 
==============================================================================================================================






Here is a list of all the request you can make 
==============================================================================================================================
===== users =====

Get "/users" -> List all users (Only User with Role Admin will get the list)


Get "/users/{id}" -> List a user by id (Only User with Role Admin will get the list)


Delete "/users/{id}" -> Delete a user by id (Only User with Role Admin will be able to delete a user)


Post "/users" -> Create user
Open the body and press raw so it says JSON den write: (
{
    "name":"<name>",
    "email":"<email>",
    "password":"<password>"
}
)
Dont forget the brackets and the comma. 


Post "/login" -> Log in as a user 
Guide:
Open the body and press raw so it says JSON den write: (
{
    "email":"<email>",
    "password":"<password>"
}
)
Dont forget the brackets and the comma. 

==============================================================================================================================
// Ej katogeriserat 
Delete "/login" -> This deletes the session/ Log out

Get "/profile" -> This shows the profile of the logged in user

==============================================================================================================================
===== Bookings =====

Get "/bookings" -> Get list of all bookings (Only User with Role Admin will get the list)


Get "/bookings/user" -> Get list of all bookings for the logged in user

==============================================================================================================================
===== countries =====

Get "/countries" -> Get list of all countries

==============================================================================================================================

===== cities =====

Get "/cities" -> Get list of all cities


Get "/cities/countries/{countryId}" -> Get list of all cities in specific country.

==============================================================================================================================
===== hotels =====

Get "/hotels" -> Get list of all hotels


Get "/hotels/cities/{cityId}" -> Get list of all hotels in specific city

==============================================================================================================================
===== activities =====

Get "/activities" -> Get list of all activities 


Get "/activities/cities/{cityId}" -> Get list of all activities in specific city

==============================================================================================================================
===== packages =====

Post "/packages" -> Create a package for the logged in user
Guide:
Open the body and press raw so it says JSON den write: (
{
    "hotelId":<the wished hotel>,
    "activityId":[<activity1>, <activity2>]
}
)
Dont forget the brackets and the comma. 


Get "/packages" -> Get a list of all packages that is exists 

==============================================================================================================================
===== Database Reset =====

Delete "/db" -> Resets database to default

==============================================================================================================================