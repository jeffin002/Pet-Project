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



alter table appointment
alter column UpdatedDateTime datetime not null

update Appointment
set UpdatedDateTime = CreatedDtTm

--Create Doctor
--CREATE PROCEDURE dbo.CreateDoctor 
--    @FirstName VARCHAR(100),
--    @LastName VARCHAR(100),
--    @Email VARCHAR(100),
--    @Description VARCHAR(100)
--AS
--BEGIN
--    INSERT INTO Doctor (FirstName, LastName, Email, Description)
--    VALUES (@FirstName, @LastName, @Email, @Description);
--END;


-->
--alter procedure dbo.GetAppointmentById    
--(    
-- @Id int    
--)    
--as    
--begin    
--select     
-- apt.Id,    
-- apt.DoctorId,    
-- apt.Description,
-- dr.FirstName+' '+dr.LastName as FullName,
-- pt.Name as petname  
--from     
-- dbo.Appointment as apt    
-- inner join dbo.Pet as pt on    
--  pt.Id = apt.PetId 
--  inner join dbo.Doctor as dr on
--  dr.Id = apt.DoctorId
--where     
-- apt.Id = @Id;    
--end


--CREATE procedure DeleteAppointment(@DeleteId int)    
--as    
--begin    
--delete from Appointment where Id = @DeleteId    
--end


--CREATE procedure dbo.GetAllAppointment  
--as  
--begin  
--select   
-- apt.Id,  
-- dtr.FirstName +' '+ dtr.LastName as DoctorFullName,  
-- pt.Name as PetName,  
-- st.Name as StatusName,  
-- apt.CreatedDtTm,  
-- apt.UpdatedDateTime  
--from  
-- dbo.Appointment as apt  
-- inner join dbo.Doctor as dtr on  
--  dtr.Id = apt.DoctorId  
-- inner join  dbo.Pet as pt on  
--  pt.Id = apt.PetId  
-- inner join dbo.Status as st on  
--  st.Id = apt.StatusId  
  
--order by  
-- apt.UpdatedDateTime  
--end


--CREATE procedure getalldoctors    
--as    
--begin    
--select Id,FirstName,LastName,FirstName+' '+ LastName as FullName from Doctor    
--order by   FullName  
--end  
  alter procedure dbo.GetAllAppointments (
	@currentPage int = 1,
	@pageSize int = 10,
	@test varchar(100) = null,
	@doctorId int = null,
	@statusId int = null
	)
as
begin
	declare @totalRecords int
	declare @tempTableAppointments table (
		Id int,
		DoctorFullName varchar(100),
		PetName varchar(100),
		StatusName varchar(50),
		CreatedDtTm datetime,
		UpdatedDateTime datetime
		)

	insert into @tempTableAppointments
	select apt.Id,
		dtr.FirstName + ' ' + dtr.LastName as DoctorFullName,
		pt.Name as PetName,
		st.Name as StatusName,
		apt.CreatedDtTm,
		apt.UpdatedDateTime
	from dbo.Appointment as apt
	inner join dbo.Doctor as dtr on dtr.Id = apt.DoctorId
	inner join dbo.Pet as pt on pt.Id = apt.PetId
	inner join dbo.status as st on st.Id = apt.StatusId
	where (
			@doctorId is null
			or apt.DoctorId = @doctorId
			)
		and (
			@statusId is null
			or apt.StatusId = @statusId
			)
	order by apt.UpdatedDateTime

	set @totalRecords = @@ROWCOUNT

	select *,
		@totalRecords as TotalRecords
	from @tempTableAppointments
	order by UpdatedDateTime
		--FETCH ONLY WORKS IF SQL SERVER IS 2012 OR ABOVE  
		OFFSET @pageSize * (@currentPage - 1) ROWS

	fetch next @pageSize ROWS only
end