--Status table
use NetTraning
go

create table dbo.Status 
(
 
   Id int not null identity(1,1)  constraint  pk_status_Id  primary key clustered,
   Name varchar(100) not null,
   CreatedDtTm datetime  constraint df_status_CreatedDtTm default(getdate())
)
insert into  dbo.Status(Name)
values('Scheduled'),('Cancelled'),('Closed')
select * from Status

--Pet type table
create table dbo.PetType
(
 
   Id int not null identity(1,1)  constraint  pk_pettype_Id  primary key clustered,
   Name varchar(100) not null,
   CreatedDtTm datetime  constraint df_pettype_CreatedDtTm default(getdate())
)


create table dbo.Breed
(
 
   Id int not null identity(1,1)  constraint  pk_breed_Id  primary key clustered,
   Name varchar(100) not null,
   PetTypeId int not null constraint fk_breed_PetTypeid foreign key references dbo.PetType(Id),
    CreatedDtTm datetime  constraint df_Breed_CreatedDtTm default(getdate())
	)

create table dbo.Doctor
(
 
   Id int not null identity(1,1)  constraint  pk_doctor_Id  primary key clustered,
   FirstName varchar(100) not null,
   LastName varchar(100) not null,
   Description varchar(100) not null,
   CreatedDtTm datetime  constraint df_doctor_CreatedDtTm default(getdate())
)

create table dbo.Pet
( 
   Id int not null identity(1,1)  constraint  pk_pet_Id  primary key clustered,
   Name varchar(100) not null,
   Description varchar(100) not null,
   BreedId int not null constraint fk_pet_breedid foreign key references dbo.Breed(Id),  
   CreatedDtTm datetime  constraint df_pet_CreatedDtTm default(getdate())
)

create table dbo.Appointment
( 
   Id int not null identity(1,1)  constraint  pk_appointment_Id  primary key clustered,   
   Description varchar(100) not null,
   DoctorId int not null constraint fk_appointment_doctorid foreign key references dbo.Doctor(Id),  
   PetId int not null constraint fk_appointment_petid foreign key references dbo.Pet(Id),  
   CreatedDtTm datetime  constraint df_appointment_CreatedDtTm default(getdate())
)
alter table dbo.appointment
add StatusId int not null constraint fk_appointment_Statusid foreign key references dbo.Status(Id)
