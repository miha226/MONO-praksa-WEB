CREATE TABLE Adress(
AdressId int PRIMARY KEY,
PostNumber int,
City varchar(15),
Street varchar (40)
);


CREATE TABLE Shop(
ShopId int  PRIMARY KEY,
Adress int FOREIGN KEY REFERENCES Adress(AdressId)
)

CREATE TABLE Person(
PersonId int PRIMARY KEY,
FirstName varchar(15),
LastName varchar(15)
)

CREATE TABLE Employee(
Person int FOREIGN KEY REFERENCES Person(PersonId),
Shop int FOREIGN KEY REFERENCES Shop(ShopId),
Position varchar(30)
);

CREATE TABLE CarModel(
CarModelId int PRIMARY KEY,
ManufacturerName varchar(20),
Model varchar(20) UNIQUE,
TopSpeed int not null
);


CREATE TABLE Car(
CarId int PRIMARY KEY,
Model int FOREIGN KEY REFERENCES CarModel(CarModelId),
StoredInShop int FOREIGN KEY REFERENCES Shop(ShopId),
DateOfManufacture int not null,
KilometersTraveled int not null,
Color varchar(20) not null
);




Insert into Adress (AdressId,PostNumber, City, Street ) Values (1, 22345, 'Osijek', 'Zagrebacka 3');
Insert into Adress (AdressId,PostNumber, City, Street ) Values (2, 14345, 'Zagreb', 'Dubrovacka 1b');
Insert into Adress (AdressId,PostNumber, City, Street ) Values (4, 34545, 'Split', 'Kralja Zvonimira 4');

Insert into Person (PersonId, FirstName, LastName) values (2341, 'Filip', 'Kovac');
Insert into Person (PersonId, FirstName, LastName) values (5612, 'Martina', 'Maric');
Insert into Person (PersonId, FirstName, LastName) values (7623, 'Ivan', 'Ivic');
Insert into Person (PersonId, FirstName, LastName) values (9783, 'Luka', 'Lukic');
Insert into Person (PersonId, FirstName, LastName) values (5723, 'Sara', 'Saric');

Insert into Shop (ShopId, Adress) values (2234,1);
Insert into Shop (ShopId, Adress) values (1432,2);
Insert into Shop (ShopId, Adress) values (3654,4);

Insert into Employee (Person,Shop,Position) values (2341, 2234, 'manager');
Insert into Employee (Person,Shop,Position) values (5612, 2234, 'salesperson');
Insert into Employee (Person,Shop,Position) values (5612, 1432, 'salesperson');
Insert into Employee (Person,Shop,Position) values (7623, 1432, 'marketing');
Insert into Employee (Person,Shop,Position) values (7623, 3654, 'marketing');
Insert into Employee (Person,Shop,Position) values (9783, 1432, 'manager');
Insert into Employee (Person,Shop,Position) values (5723, 3654, 'manager');

Insert into CarModel (CarModelId, ManufacturerName, Model, TopSpeed) values (23456, 'Audi', 'A1', 200);
Insert into CarModel (CarModelId, ManufacturerName, Model, TopSpeed) values (23457, 'Audi', 'A3', 220);
Insert into CarModel (CarModelId, ManufacturerName, Model, TopSpeed) values (23458, 'Audi', 'A4', 220);
Insert into CarModel (CarModelId, ManufacturerName, Model, TopSpeed) values (23459, 'Audi', 'A6', 250);

Insert into CarModel (CarModelId, ManufacturerName, Model, TopSpeed) values (33421, 'BMW', '120d', 220);
Insert into CarModel (CarModelId, ManufacturerName, Model, TopSpeed) values (33522, 'BMW', '220d', 250);
Insert into CarModel (CarModelId, ManufacturerName, Model, TopSpeed) values (33620, 'BMW', '300d', 280);

Insert into CarModel (CarModelId, ManufacturerName, Model, TopSpeed) values (11421, 'VW', 'Golf', 220);
Insert into CarModel (CarModelId, ManufacturerName, Model, TopSpeed) values (12421, 'VW', 'Passat', 250);

Insert into Car (CarId,Model,StoredInShop,DateOfManufacture,KilometersTraveled,Color) values (1, 11421, 2234, 2015, 190000, 'White'); 
Insert into Car (CarId,Model,StoredInShop,DateOfManufacture,KilometersTraveled,Color) values (2, 33522, 2234, 2014, 140000, 'Grey');
Insert into Car (CarId,Model,StoredInShop,DateOfManufacture,KilometersTraveled,Color) values (4, 23457, 1432, 2011, 110000, 'Grey');
Insert into Car (CarId,Model,StoredInShop,DateOfManufacture,KilometersTraveled,Color) values (8, 23456, 1432, 2019, 90000, 'Black');
Insert into Car (CarId,Model,StoredInShop,DateOfManufacture,KilometersTraveled,Color) values (12, 33620, 3654, 2022, 20000, 'White');
Insert into Car (CarId,Model,StoredInShop,DateOfManufacture,KilometersTraveled,Color) values (14, 33620, 3654, 2021, 120000, 'Black');


Select * from Car where Model=33620;

Select * from CarModel order by ManufacturerName DESC;

Select FirstName, LastName, Employee.Position from Person
full outer join Employee on Person.PersonId=Employee.Person;

Select Position, COUNT(Position) as NumberOfEmployees from Employee group by Position having COUNT(Position)>2;

Select Car.Model, AVG(Car.KilometersTraveled) from Car group by Car.Model;

Select CarModel.Model, AVG(Car.KilometersTraveled) as 'AvgKilometersTraveled'
from Car left join CarModel on CarModel.CarModelId=Car.Model group by CarModel.Model order by CarModel.Model ASC;

Select * from Car;
Update Car set Color='Brown' where KilometersTraveled between 10000 and 100000;
Delete from Car where Color='White';

Select TOP 2 * from Car;
Select * from Person;
Alter table Person add Balance int;
Alter table Person drop column Balance;
