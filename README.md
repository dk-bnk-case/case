# Buying & Owning Real Estate Nordic Case

## Unanswered questions for the task:

* The tax schedules doesn't seem to have a clear priority, eg. which tax schedule to chose first when multiple can be valid.
  * The examples highlight a potential issue where 2016.01.01 is valid for 0.2 and for 0.1
  * I will argue that the most precise date match takes priority, or that it is sorted by Day, Week, Month & Year and both would result in the same result given in the case.
  * I would validate my questions to business to make sure that the right algorithm is used, and also question if this algorithm would change over time, potentially it would be beneficial to introduce a set of rules.
* When inserting tax schedules, what to do when the values imported already exists? - in this solution I wont handle it.
* When inserting tax schedules, what to do when the values are invalid? - in this solution there's limited error handling.

## General info
The solution has been created primarily with docker, docker-compose and Visual Studio Code.
Using docker it is possible to create clean working environments where you can trust it to replicate the behavior on other machines (no more it works on my machine). However there are still small differences in behavior when running docker on top on eg. Linux vs Windows. I have tried to avoid any of the pitfals, but no guarantee can be given.

### The algorithm
As mentioned in the questions it is not clear how the algorithm should work, and I would never try to deal with this in the real world. Instead validation of the different options should be done before proceeding with implementation.
I'll assume this has been done and that ordering by day, week, month, year is the correct one in case of overlap with dates.
The Algorithm works by taking an input String and date. It will match the String to a municipality (or give a not found, or invalid input for some cases).
Then it will match the input date to the returned values that are sorted by as described above where it will try and match it as follows:
> input >= start_range and input <= end_range ordered by daily, weekly, monthly, yearly

This first match is chosen and the tax is returned.

### Client
The Client will need to validate input format and send the request to the webapi and await a result.
A simple way is a console application which listens for user input.

### API
In order to simplify the api I have containerized it and given it an optional file argument where it will load the contents of a filepath (if possible)
It supports 3 operations, Get, Get by id, Get by Municipality name and date and Post to save a new Municipality
There's a swagger documentation available as well on http://localhost:8080/swagger/index.html if you run it with default settings.

### Storage
In order to have persistense a simple ORM setup with entity framework and a postgresql database is added.
This part is mostly configuration and very little code.
Querying is done directly with no regards to locks etc.
The database is initialized upon first launch using the `dbscripts/seed.sql` file.
If you need to change it, it is necessary to remove the docker volume as postgresql is setup to ignore the seed if data is already present in the volume. It will instead load the persisted data in the volume.

If you need to remove the persisted volume you can issue: `docker-compose down -v`

# Running the solution
In the root of the folder is 2 docker files and 1 docker-compose file.
The docker-webapi file is to made to build and run the webapi part of the solution and the docker-client is made to build the client part.
The docker-compose file connects the webapi with the database and "readies" the backend for consumption.

In order to build the solution issue `docker-compose build`
In order to interact with the client run `docker-compose run client sh` It will also spin up the webapi and postgresql
Afterwards you can interact with the client directly in the console. You can also checkout the swagger documentation available on http://localhost:8080/swagger/index.html
Finally using any postgresql tool you can also interact with the DB and validate persistence directly in the DB.
The connection strings are listed in the `docker-compose.yml` file

It is also possible to interact directly with the webapi through the swagger interface available on http://localhost:8080/swagger/index.html if you run it with default settings.

## TODO

I failed to notice the last details of the case where it mention to do this with the consumer/producers. In order to do it this way I would introduce a queue (most likely a simple setup using rabbitmq) and place the webapi and client on either side of the queue.
I've used https://masstransit-project.com/ in the past for similar things.

I've made an interactive client (console application) for testing and it's possible to add functionality such as to take entire records as a parameter both for querying and for persisting new records. I have left this out for now.

Cleanup, the client could use some cleanup.
