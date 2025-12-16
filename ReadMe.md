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


======================================================================================================================
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