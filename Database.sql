Create database [Sample]

use [Sample]

create table Register(Id int Identity(1,1) primary key , FullName nvarchar(150) not null , UserName nvarchar(150) not null ,Password nvarchar(150) not null)

create table Employee(Id int Identity(1,1) primary key , FullName nvarchar(100) not null , Age int not null , DepartmentId int not null)
