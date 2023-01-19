CREATE TABLE Adress(
AdressId uniqueidentifier PRIMARY KEY default newID(),
PostNumber int,
City varchar(15),
Street varchar (40)
);


CREATE TABLE Shop(
ShopId uniqueidentifier PRIMARY KEY default newID(),
ShopName varchar(30) not null,
Adress uniqueidentifier FOREIGN KEY REFERENCES Adress(AdressId) UNIQUE
)

select * from Shop left join Adress on Shop.Adress=Adress.AdressId
 full outer join Car on Shop.ShopId=Car.StoredInShop  where ShopId='F3905678-3478-4262-9048-3845CDC998B6'

select * from Adress
Insert into Shop (ShopName ,Adress) values ('test2','81BB34FC-A2C4-4FD6-A0BB-684D051927C8');


CREATE TABLE Person(
PersonId uniqueidentifier PRIMARY KEY default newID(),
FirstName varchar(15),
LastName varchar(15)
)

CREATE TABLE Employee(
Person uniqueidentifier FOREIGN KEY REFERENCES Person(PersonId),
Shop uniqueidentifier FOREIGN KEY REFERENCES Shop(ShopId),
Position varchar(30)
);





CREATE TABLE Car(
CarId uniqueidentifier PRIMARY KEY default newID(),
StoredInShop uniqueidentifier FOREIGN KEY REFERENCES Shop(ShopId),
YearOfManufacture int not null,
KilometersTraveled int not null,
Color varchar(20) not null,
ManufacturerName varchar(20) not null,
Model varchar(20) not null,
TopSpeed int not null
);



Insert into Car (ManufacturerName,Model,YearOfManufacture,KilometersTraveled,TopSpeed,Color,StoredInShop) 
values ( 'Audi','A6',2012, 100000, 220, 'White', '283409BF-BB9B-4DE8-9284-198BA2923A3A')
select * from Car
delete from Car where CarId='D3468011-68CF-4AA4-BB79-1C76DDAE8BC7'

drop table Car
drop table Employee
drop table Shop
drop table Adress
drop table Person


Insert into Adress (PostNumber, City, Street ) Values ( 22345, 'Osijek', 'Zagrebacka 3');
Insert into Adress (PostNumber, City, Street ) Values ( 14345, 'Zagreb', 'Dubrovacka 1b');
Insert into Adress (PostNumber, City, Street ) Values ( 34545, 'Split', 'Kralja Zvonimira 4');

Insert into Person ( FirstName, LastName) values ( 'Filip', 'Kovac');
Insert into Person ( FirstName, LastName) values ( 'Martina', 'Maric');
Insert into Person ( FirstName, LastName) values ( 'Ivan', 'Ivic');
Insert into Person ( FirstName, LastName) values ( 'Luka', 'Lukic');
Insert into Person ( FirstName, LastName) values ( 'Sara', 'Saric');
select * from Shop




Insert into Employee (Person,Shop,Position) values (2341, 2234, 'manager');
Insert into Employee (Person,Shop,Position) values (5612, 2234, 'salesperson');
Insert into Employee (Person,Shop,Position) values (5612, 1432, 'salesperson');
Insert into Employee (Person,Shop,Position) values (7623, 1432, 'marketing');
Insert into Employee (Person,Shop,Position) values (7623, 3654, 'marketing');
Insert into Employee (Person,Shop,Position) values (9783, 1432, 'manager');
Insert into Employee (Person,Shop,Position) values (5723, 3654, 'manager');







select * from CarModel
Insert into Car (Model,StoredInShop,YearOfManufacture,KilometersTraveled,Color) 
values ( (Select CarModelId from CarModel where Model='A6'),
'283409BF-BB9B-4DE8-9284-198BA2923A3A', 2012, 220000, 'Grey'); 
select * from car

Select CarId,CarModel.ManufacturerName, CarModel.Model,Car.StoredInShop, CarModel.TopSpeed, Car.YearOfManufacture, KilometersTraveled,
Color from Car left join CarModel on Car.Model = CarModel.CarModelId

Select CarId,CarModel.ManufacturerName,Car.StoredInShop, CarModel.Model, CarModel.TopSpeed, 
Car.YearOfManufacture, KilometersTraveled, Color from Car left join CarModel on Car.Model = CarModel.CarModelId
delete from Car where CarId='9AFDF525-55B2-4CA7-9D48-B1F7D2FFEC5E'

Select * from Car left join CarModel on Car.Model = CarModel.CarModelId

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



drop table Car
