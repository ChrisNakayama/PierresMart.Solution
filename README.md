# PierresMart

#### By _**Chris Nakayama**_

#### _A web application for Pierre._

## Technologies Used

C#
.NET 5.0
ASP.NET Core
CSS
Bootstrap
Razor View Engine
Entity Framework Core
Description
This simple web application shows a many to many relationship with a database in SQL with Authorization and added functinality of sign in to access edit and delete.

Setup Instructions
Open your terminal and navigate to the folder you'd like to download the files into.
Run the command below
git clone https://github.com/ChrisNakayama/PierresMart.Solution

Download MySQL Workbench and use it to create a local instance and an associated password.
Within the directory ~/PierresMart, create a file named appsettings.json and input the lines of code bellow.
{ "ConnectionStrings": { "DefaultConnection": "Server=localhost;Port=3306;database=Pierres_Mart;uid=root;pwd=epicodus;" } }

Replace [YOUR-PASSWORD] with the password you created within MySQL Workbench and make sure the port matches your own.
Navigate to ~/PierresMart in your terminal.
Run the commands below
dotnet tool install --global dotnet-ef --version 3.0.0

dotnet add package Microsoft.EntityFrameworkCore.Design -v 5.0.0

dotnet ef database update

dotnet build

dotnet run

Copy and paste the local host URL provided in the terminal into a web browser and enjoy!
Known Bugs
No known bugs at this time.
License
Copyright (c) 03/31/2022 by Chris Nakayama

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.