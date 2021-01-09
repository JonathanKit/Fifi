# FIFI the Film Finder

January 2021

![](RackMultipart20210109-4-15ytdkh_html_41c422f6a5c7e025.gif)

Sometimes it can be so hard to find a movie that suits to watch, especially when watching a movie with more people. So, the aim of this project is to have a program where the watcher can set its favourable attributes of the movie and the program will deliver a movie for a perfect movie-night.

**The program is written in C# with WPF, ReactiveUI and Fody in a MVVM style**

Author: Jonathan Helmond

## Overview

The program is built up on a MVVM style, which means that you have a view, a view model and a model. By doing so the program has a clear structure and the view is independent which provides the possibility to extend the view from only WPF to XAMARIN more easily.

## Database

To achieve this the first step is to have a database containing all the necessary information of all the movies existing. The database used is a MySQL database that contains all the movies until 2019.

More information about the database can be found here: [https://github.com/JonathanKit/movie\_database](https://github.com/JonathanKit/movie_database)

The database is used by a connection in the C# project using the MySql.Data.MySqlClient library. A connection can be made by:

connection=newMySqlConnection(&quot;Server=&#39;localhost&#39; User=&#39;root&#39;,…);

A query can then be sent by:

MySqlCommand(query, connection);

This all happens in the DBhelper classes that contains the classes for creating movie objects containing all the information of a movie and saving them in a list of movie objects. On the other side this classes contain the functions to establish the connection, run the query and use specific filters for the creating the SQL command.

## View

The View is made with WPF whereas the code is written in XAML.

To have a constant design in the GUI a design dictionary(dictionary1) is used which contains universal information of design. 



In the XAML file the design is created with panels. There are different types of panels that have different attributes. As the first Panel a Dockpanel is used that dock its element into the window. A Stackpanel is for example used when having all the block for choosing the genre that should take as less space as possible. Each element in the view that later must be read or written needs a unique name so it can be accessed in the backend code.

In the backend there is a class for the view that can access the view and the Viewmodel to connect them and set up the bindings.

this.Bind(ViewModel, vm =\&gt; vm.searchtext, v =\&gt; v.searchtextbox.Text).DisposeWith(d);

This line for example sets up the binding between the text of the textbox in the view and the Viewmodel.

## Viewmodel

    Searchbutton = ReactiveCommand.Create(() =\&gt;{
    //Console.WriteLine(&quot;button pressed&quot;);
    Clear\_movies();
    search();
    ShowMovies();
    });

In the Viewmodel there are all variables that are needed for the program. Since Reactive UI is used for this program the variables are reactive variables that are set corresponding when the variable in the view is changed. Also, the functions are here that must be executed on command. In Reactive UI these are called ReactiveCommand. The main idea behind Reactive UI is to execute this reactively. For example, for the search of a movie the button press event can be attached to a reactive command that gets executed every time the button is pressed. This looks like the following:

Whereas the function Clear\_movies clears the old movies from the last recent search. The search function searches for the movies that have the name searching for. And the Show movies function shows the found movies in the view. The functions connected to the database are defined in the DB class.

The movies that are retrieved by the search are processed by calling them from the database with the C# connector, storing them in a movie object and then storing these movie objects in a list of movies.
