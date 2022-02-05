COSC2276 / COSC2277 Assignment 2 Completed by YehHaw Teh (s3813866) and Andrew Nhan Trong Tran (s3785952)

GitHub: https://github.com/rmit-wdt-fs-2022/s3813866-s3785952-a2

We have completed the Pass (PA), Credit(CR) and, Distinction (DI) and some (HD) level of the assignment. 

**Things about the project** 
There are 3 projects in the zip folder, "Assignment 2", "Assignment2APIs" and, "Assignment2ClassLibrary". 
* "Assignment 2" contains the code for Pass and Credit levels while 
* "Assignment2APIs" contains the APIs required for Distinction. 
* "Assignment2ClassLibrary" is a class library that contains the model and utility methods that are used in the
other two projects.


**What We Have Done**
For our [PA] level, we have successfully ensured that if the tables from the database are not yet populated, we 
are calling the web API and deserializing the data.  For our home page, we have decided to have one page
which manages all the accounts of that user that is logged in and attach a button to their request.  Once they 
click on a button to go to their page, the account number will be passed, which can be then used within the controller.
We have also set up sessions in which if the user were to refresh the page and revisit the page, 
they would be logged in given that their cookies have not yet been evident over a certain amount of time.  We have 
also implemented my statement page, which shows all the transactions of the logged-in user, and ensured that only 4 
transactions were revealed at a time, with the time being ordered by time.  We have also implemented my profile page, 
which shows the currently logged-in users' details and what they can update.  There is a lot of validation within
this component as we want to ensure that the customer enters only strings or ints depending on what is asked.  If a
field is invalid, an error message will appear, and the database will not update once all fields are filled out and are 
correct.  Changing password also ensures that both password and confirmation password are the same, then will be hashed
and saved back into the login database. 

For our [CR] we ensured background task runs every 10 seconds and will delete a bill pay or add into the transaction
table.  Checks are also in place to ensure that the correct payeeId or account number entered are correct so payment 
can be scheduled.  There are also checks for different account types if an account is checking the balance at the end
of the scheduled payment has to be >= 300 if not this will be a failed bill pay.

In [DI]/[HD] level, we do not have time to finish Admin portal. However we did finished implementing 
some of the WebAPIs required.  During our development cycle we also did some parts of the HD section which
includes some of the backend logic decoupled from the controllers which we added most of the validation 
inside the models.  As seen my profile controller the two methods that are getting the data and updating 
data there is no logic to check for whether the fields entered were correct as this is done within the 
model.  Within the Customer model which is within the class library.

![image](https://user-images.githubusercontent.com/57212703/152641436-fd712ac7-3e08-48b2-8c66-abaa66cbeabf.png)

![image](https://user-images.githubusercontent.com/57212703/152641472-0a76b797-1d79-42e6-b292-9f1130b38427.png)

