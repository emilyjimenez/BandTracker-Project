
# _Band Tracker_

## _Independent Project - Epicodus C# Week 4_

#### By _Emily Wells Jimenez_

###### _11.3.2017_

## Description

_Web app designed to store band and music venue information. A user can add bands/venues, and connect bands that have played at a venue, and venues that have had bands perform._



## Specs

| Behavior  |  Input | Output  |
|---|---|---|
| Allow user to add a venue  | Meow Meow, Portland OR  | Venue Name: Meow Meow, Venue City and State: Portland, OR  |
| Allow user to add a band  | Thursday, emo  | Band Name: Thursday, Genre: Emo  |
| User can add bands to venue, user can add venues to band  | User selects band or venue from drop down on detail page and clicks "add"  | Band or Venue is then added to the corresponding detail page |
| Allow user to update venue  | user clicks "update venue" and fills out from  | Venue detail page shows updated information  |
| Allow user to delete venue  | user clicks "delete venue"  | upon returning to venues page, venue will no longer be displayed  |


## MySql Commands

> * CREATE DATABASE band_tracker;
> * USE band_tracker
> * CREATE TABLE bands(id serial PRIMARY KEY, name VARCHAR(255), genre VARCHAR(255));
> * CREATE TABLE venues(id serial PRIMARY KEY, name VARCHAR(255), address VARCHAR(255));
> * CREATE TABLE bands_venues(id serial PRIMARY KEY, band_id INT, venue_id INT);

**Note** _Remember to run these commands for a band_tracker_test database too!_

## Setup/Installation Requirements

1. Clone [BandTracker-Project](https://github.com/emilyjimenez/BandTracker-Project) from Github
2. Make sure you have MAMP installed and are using .NET Core 1.1
3. Turn servers on via MAMP, and then load band_tracker and band_tracker_test databases into the PHPmyadmin tool in MAMP
4. Run dotnet restore and build on both the HairSalon folder and BandTracker.Tests folder, then run dotnet test on the test folder
5. After restore/build/test, run the dotnet run command on the BandTracker folder on your terminal and go to localhost5000 in your preferred web browser

## Known Bugs

NA

## Technologies Used

* Atom
* C#
* Mono
* MySQL
* .Net Core 1.1
* MAMP
* PHPmyadmin
* Google Chrome

### License

This software is licensed under the MIT license.

Copyright (c) 2017 Emily Wells-Jimenez, EWJ Consulting
