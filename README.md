## TodoTasker - a todo list application  <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg" alt="csharp" width="40" height="40"/>

This is the first individual project for the C# console application course at Lexicon. We were given 2 choices, a money tracking app and a todo list application, and I chose the todo list. 

#### *Assigment description*
The application was meant as a way to let the user add tasks, with title, due date and a project for where the task belong to. It was to list the tasks, ordered by project or date, and showing the status of each tasks. The user was to use a text based user interface via command line. From there the user can also edit, remove or mark the task as done. The task list where to be saved to a file then loaded back in at app restart.

#### *Requirements*
A clear list of requirements for the application

* Model a task with a task title, due date, status and project  
* Display a collection of tasks that can be sorted both by date and project  
* Support the ability to add, edit, mark as done, and remove tasks
* Support a text-based user interface
* Load and save task list to file 


#### steps in the development of the app
This are some of the steps in development of the app
1. Define a `TodoTask` class with properties for title, due date, status, and project.
2. Create a `TaskManager` class to manage a list of tasks, including methods for adding, removing, editing, marking as done.
3. Implement methods in `TaskManager` to sort tasks by date and project.
4. Create a console application with a menu-driven interface to interact with the user.
5. Use JSON serialization for saving and loading tasks to/from a file.
6. Further interface design (text decorations and submenus).
7. Implement error handling for user inputs and file operations. Create a sample list with tasks if no saved list is found

The teacher supplied us with a sample output mockup image and 2 file handling files that we could use. But since it has been a while since I worked with programming, I tried to make those features bit by bit. I asked AI to make me a code to save and load the task list from a json file. When I got it working I could better understand what the code in the files provided do, and I ended up using a lot of it anyway. Like the feature of creating a sample list with some tasks if no saved list is find during loading. This lead me into adding a constructor in the TodoTask class.
Bellow are some screen grabs from the end result:

![Skärmbild 2025-06-11 141243](https://github.com/user-attachments/assets/037923aa-8596-45c7-8e30-d391e27161a3)
*Main menu*
<br /><br /><br />
![Skärmbild 2025-06-11 141058](https://github.com/user-attachments/assets/60714351-b358-4048-beec-227f241a5735)
*Show task list submenu and the list of tasks, sorted by projects*

Also, I got inspired by the way a ColoredText class was used to color the texts. I made my own and named it DecoratedText, with methods for displaying "bullet points" (">>") and numbered lists to match the mockup, and also for displaying the output text with a certain color after the "bullet". 



#### *Improvments*
There is a lot of room for improvment, but It's good enough in my opinion. I tried to keep all the-output-to-console -texts in the main program and separated from the other classes, and did so except for the messages that outputs according to the status of the loading of tasks.
Another thing would be to keep the file handling code in a separate class and file. And maybe prompt the user to save the task list uppon adding or editing a task.
For the next project I should be better in the planning stage and start by making UML diagrams. It has been over 20 years since I used it and at the time this is written I have yet to learn any UML diagram tools. Back in the day I learned about it was pen on paper.

<p>
That's all for now, happy grading!

Cheers! <br />
*/Danny Gomez*
</p>

