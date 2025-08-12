use master
go

Create Database Freelance
Go

use Freelance
Go

Create Table LoginInfo(
   LoginId int IDENTITY(1,1) Primary Key,
   Email varchar(max),
   Password varchar(max)
);

Create Table Users(
	UserId int IDENTITY(1,1) Primary Key,
	LoginId int Foreign Key references LoginInfo(LoginId) On Delete Cascade On Update Cascade,
	UserFName varchar(30),
	UserLName varchar(30),
	UserAge int,
	UserGender varchar(50) Check(UserGender in ('Male','Female','Other')),
	UserType varchar(50) Check(UserType in ('Freelancer','Client'))
);

Create Table Clients(
	ClientId int Foreign Key references Users(UserId) On Delete Cascade On Update Cascade,
	Domain varchar(max),         -- e.g. Web Dev, Game Dev , Discrete, LA
	RequiredSkills varchar(max), -- e.g. C++, Python, MERN  For Freelance Suggestions
	Primary Key(ClientId)
);

Create Table Tasks(
	TaskId int IDENTITY(1,1),
	ClientId int Foreign Key references Clients(ClientId) On Delete Cascade On Update Cascade,
	TaskType varchar(50),           -- e.g. Assignment, Project, Quiz, FYP Help
	TaskTitle varchar(100),
	TaskDescription varchar(max),
	TaskRequirements varchar(max),  -- e.g. Skills or Educaton of Freelancer
	TaskDeadline Date,
	Primary Key(ClientId, TaskId)
);

Alter Table Tasks
add TaskBudget float;

Alter Table Tasks
add CompletionStatus int;

Alter Table Tasks
Add Constraint CMP_Default
Default 0 for CompletionStatus;

Create Table Freelancer(
	FreelancerId int Foreign Key references Users(UserId) On Delete Cascade On Update Cascade,
	Domain varchar(max),         -- e.g. Web Dev, Game Dev , Discrete, LA
	Summary varchar(max),
	ProfficientSkills varchar(max),
	IntermediateSkills varchar(max),
	FamiliarSkills varchar(max),
	Primary Key(FreelancerId)
);

Create Table Education(
	EducationId int IDENTITY(1,1),
	FreelancerId int Foreign Key references Freelancer(FreelancerId) On Delete Cascade On Update Cascade,
	Institute varchar(max),
	Degree varchar(max),
	Program varchar(max),
	StartDate Date,
	EndDate Date,
	Primary Key(FreelancerId, EducationId)
);

Create Table Proposals(
	ProposalId int IDENTITY(1,1),
	FreelancerId int Foreign Key references Freelancer(FreelancerId),
	ClientId int,
	TaskId int,
	Foreign Key(ClientId, TaskId) references Tasks(ClientId, TaskId) On Delete Cascade On Update Cascade,
	Primary Key(FreelancerId, ProposalId, ClientId, TaskId)
);

Alter Table Proposals
Add constraint Pk_Proposal
Primary Key(FreelancerId, ClientId, TaskId);

Alter Table Proposals
add Request varchar(max),
    Links nvarchar(max);

Alter Table Proposals
add BidAmount float;

Create Table Portfolio(
	PortfolioId int IDENTITY(1,1),
	FreelancerId int Foreign Key references Freelancer(FreelancerId) On Delete Cascade On Update Cascade,
	ImageURL nvarchar(max),
	ProjectDescription varchar(max),
	Primary Key(FreelancerId, PortfolioId)
);

Alter Table Portfolio
add VideoURL nvarchar(max);

Create Table Rating(
	RatingId int IDENTITY(1,1),
	ClientId int,
	TaskId int,
	RatingStar int,
	RatingReview varchar(max),
	Foreign Key(ClientId, TaskId) references Tasks(ClientId, TaskId) On Delete Cascade On Update Cascade,
	Primary Key(RatingId, ClientId, TaskId)
);

Select * from LoginInfo;
Select * from Users;
Select * from Clients;
Select * from Tasks;
Select * from Freelancer;
Select * from Education;
Select * from Proposals;
Select * from Portfolio;
Select * from Rating;

Insert into LoginInfo(Email, Password)
values('arhamzeeshan617@gmail.com', 'playstation');

Insert into Users(LoginId, UserFName, UserLName, UserAge, UserGender, UserType)
values(2, 'Arham', 'Zeeshan', 20, 'Male', 'Client');

Insert into Clients
values(2, 'Web Development', 'HTLML, CSS, MERN');

Insert into Tasks(ClientId, TaskType, TaskTitle, TaskDescription, TaskRequirements, TaskDeadline)
values(2, 'Project', 'Business Portfolio Site', 'Full Stack Ecommerce Site with MERN along with SQL and ....', 'Experience with MERN Stack, Portfolio, Animations', '2025-08-29');

Insert into Freelancer
values(1, 'Game Dev', 'Professional 2d Game Developer with 2 years of experience', 'Unity', 'Unreal', 'C++, SFML');

Insert into Education(FreelancerId, Institute, Degree, Program, StartDate, EndDate)
values(1, 'FAST Lahore', 'Bachelor', 'CS', '2023-07-20', '2027-07-20' );

Insert into Proposals(FreelancerId, TaskId, ClientId, Request, Links)
values(1, 1, 2, 'I am the best option you have', 'https:localhost.skibidi');


-- Stored Procedures
-- 1, Sign In
Go
Create Procedure Signin
@fname varchar(50),
@lname varchar(50),
@age int,
@gender varchar(30),
@type varchar(30),
@email varchar(max),
@password varchar(max),
@userId int output
as begin

Insert into LoginInfo(Email, Password)
values(@email, @password);

declare @loginId int;
set @loginId = SCOPE_IDENTITY();

if @gender in ('Male','Female','Other') and @type in ('Freelancer','Client')
begin
	Insert into Users(LoginId, UserFName, UserLName, UserAge, UserGender, UserType)
	values(@loginId, @fname, @lname, @age, @gender, @type);

	set @userId = SCOPE_IDENTITY();
end

else
begin
	set @userId = -1;
end

end

declare @UserOutputId int; 
exec Signin 'Ahmed','Aslam',20,'Male','Freelancer','AhmedAslam123@gmail.com','hello', @UserOutputId output;
print(@UserOutputId);

Select * from LoginInfo;
Select * from Users;

-- 2, Freelancer Details after Signin
Go
Create Procedure FreelancerDetails
@freelancerId int,
@domain varchar(max),
@summary varchar(max),
@profficientSkills varchar(max),
@intermediateSkills varchar(max),
@familiarSkills varchar(max)
as begin

Insert into Freelancer
values(@freelancerId, @domain, @summary, @profficientSkills, @intermediateSkills, @familiarSkills);

end

--3, Client Details after Signin
Go
Create Procedure ClientDetails
@clientId int,
@domain varchar(max),
@requiredSkills varchar(max)
as begin

Insert into Clients
values(@clientId, @domain, @requiredSkills);

end

--4, Loggin User
Go
Create Procedure LogginUser
@email varchar(max),
@password varchar(max),
@userId int output,
@userType varchar(max) output
as begin

if exists(Select 1 from LoginInfo L
		  join Users U
		  On L.LoginId = U.LoginId
		  where L.Email = @email and L.Password = @password)
begin
	Select @userId = U.UserId, @userType = U.UserType from LoginInfo L
	join Users U
	On L.LoginId = U.LoginId
	where L.Email = @email and L.Password = @password;
end

else
begin
	set @userId = -1;
	set @userType = 'NotFound';
end

end

declare @UserOutputId int;
declare @UserOutputType varchar(max);
exec LogginUser 'arhamzeeshan617@gmail.com', 'playstation', @UserOutputId output, @UserOutputType output;
print(@UserOutputId);
print(@UserOutputType);

Select * from LoginInfo;

--5, Add Client Tasks
Go
Create Procedure AddTasks
@clientId int,
@taskType varchar(max),
@taskTitle varchar(max),
@taskDescription varchar(max),
@taskRequirements varchar(max),
@taskDeadline Date,
@taskBudget float,
@flag int output
as begin

if exists(Select 1 from Clients where ClientId = @clientId)
begin
	Insert into Tasks(ClientId, TaskType, TaskTitle, TaskDescription, TaskRequirements, TaskDeadline, TaskBudget)
	values (@clientId, @taskType, @taskTitle, @taskDescription, @taskRequirements, @taskDeadline, @taskBudget);

	set @flag = 1;
end

else
begin
	set @flag = 0;
end

end

declare @OutputFlag int;
exec AddTasks 1, 'Assignment','OOP', 'Assignment On Inheritance and Polymorphism', 'Person with strong OOP and C++ skills', '2025-08-30', 500, @OutputFlag output
print(@OutputFlag)

Select * from Tasks;

--6 Update Client Tasks
Go
Create Procedure UpdateTasks
@clientId int,
@taskId int,
@taskType varchar(max),
@taskTitle varchar(max),
@taskDescription varchar(max),
@taskRequirements varchar(max),
@taskDeadline Date,
@taskBudget float,
@flag int output
as begin

if exists (Select 1 from Tasks where ClientId = @clientId and TaskId = @taskId)
begin
	Update Tasks
	set TaskType = @taskType, TaskTitle = @taskTitle, TaskDescription = @taskDescription, TaskRequirements = @taskRequirements,
	TaskDeadline = @taskDeadline, TaskBudget = @taskBudget
	where ClientId = @clientId and TaskId = @taskId;

	set @flag = 1;
end

else
begin
	set @flag = 0;
end

end

--7, Delete or Remove Client Task
Go
Create Procedure DeleteClient
@taskId int,
@clientId int,
@flag int output
as begin

if exists(Select 1 from Tasks where ClientId = @clientId and TaskId = @taskId)
begin
	Delete from Tasks
	where TaskId = @taskId and ClientId = @clientId;

	set @flag = 1;
end

else
begin
	set @flag = 0;
end

end

declare @DeleteTaskFlag int;
exec DeleteClient 3, 1, @DeleteTaskFlag output;
print(@DeleteTaskFlag);

Select * from Tasks;

--8, Add Freelancer Education
Go
Create Procedure AddEducation
@freelancerId int,
@institute varchar(200),
@degree varchar(100),
@program varchar(100),
@startDate Date,
@endDate Date
as begin

if exists(Select 1 from Education where @freelancerId = FreelancerId)
begin
	Insert into Education(FreelancerId, Institute, Degree, Program, StartDate, EndDate)
	values (@freelancerId, @institute, @degree, @program, @startDate, @endDate);
end

end

--9, Delete Freelancer Education
Go
Create Procedure DeleteEducation
@educationId int
as begin

Delete from Education
where EducationId = @educationId;

end

--10, Update or Edit Freelancer Education
Go
Create Procedure UpdateEducation
@freelanceId int,
@educationId int,
@institute varchar(200),
@degree varchar(100),
@program varchar(100),
@startDate Date,
@endDate Date
as begin

if exists(Select 1 from Education where FreelancerId = @freelanceId and EducationId = @educationId)
begin
	Update Education
	set Institute = @institute, Degree = @degree, Program = @program,
	StartDate = @startDate, EndDate = @endDate;
end

end

--11, Add Freelancer Proposal
Go
CREATE PROCEDURE AddProposal
    @freelancerId INT,
    @taskId INT,
    @request VARCHAR(MAX),
    @links NVARCHAR(MAX),
    @bidAmount float,
    @flag INT OUTPUT
AS
BEGIN
    DECLARE @taskBudget float, @clientId INT;

    SELECT @taskBudget = TaskBudget, @clientId = ClientId
    FROM Tasks
    WHERE TaskId = @taskId;

    IF @taskBudget IS NULL
    BEGIN
        SET @flag = -2; -- Task does not exist
    END
    ELSE IF @bidAmount <= 0
    BEGIN
        SET @flag = 0; -- Invalid amount
    END
    ELSE IF @bidAmount < @taskBudget * 0.7
    BEGIN
        SET @flag = -1; -- Less than 70% of budget
    END
    ELSE IF @bidAmount > @taskBudget * 1.5
    BEGIN
        SET @flag = 1; -- More than 150% of budget
    END
    ELSE
    BEGIN
        INSERT INTO Proposals (FreelancerId, ClientId, TaskId, Request, Links, BidAmount)
        VALUES (@freelancerId, @clientId, @taskId, @request, @links, @bidAmount);

        SET @flag = 2; -- Success
    END
END;

declare @ProposalOutputFlag int;
exec AddProposal 1, 1, 'I woud like to assist you in your task', 'https:something.xyz', 100, @ProposalOutputFlag output;
print(@ProposalOutputFlag);

Select * from Proposals;

--12, Delete Proposal
Go
Create Procedure DeleteProposal
	@proposalId int
as begin
	Delete from Proposals
	where ProposalId = @proposalId;
end

--13, Add Freelancer Portfolio
Go
Create Procedure AddPortfolio
	@freelancerId int,
	@imageURL nvarchar(max),
	@projectDescription varchar(max),
	@videoURL nvarchar(max)
as begin
	if exists(Select 1 from Freelancer where FreelancerId = @freelancerId)
	begin
		Insert into Portfolio(FreelancerId, ImageURL, ProjectDescription, VideoURL)
		values(@freelancerId, @imageURL, @projectDescription, @videoURL);
	end
end

exec AddPortfolio 1, 'something.png', 'A Budget Tracking web and mobile app for students', 'something.mp4';

Select * from Portfolio;

--14, Delete Portfolo
Go
Create Procedure DeletePortfolio
	@portfolioId int
as begin
	if exists(Select 1 from Portfolio where PortfolioId = @portfolioId)
	begin
		Delete from Portfolio
		where PortfolioId = @portfolioId;
	end
end

--15, Update Portfolio
Go
Create Procedure UpdatePortfolio
	@portfolioId int,
	@imageURL nvarchar(max),
	@projectDescription varchar(max),
	@videoURL nvarchar(max)
as begin
	Update Portfolio
	set ImageURL = @imageURL,
		ProjectDescription = @projectDescription,
		VideoURL = @videoURL
	where PortfolioId = @portfolioId
end

--16, Add Rating
Go
Create Procedure AddRating
	@taskId int,
	@ratingStar int,
	@ratingReview varchar(max)
as begin
	if exists(Select 1 from Tasks where TaskId = @taskId and CompletionStatus = 0)
	begin
		declare @clientId int;
		Select @clientId = ClientId from Tasks
		where TaskId = @taskId;

		if(@ratingStar>= 0 or @ratingStar<=5)
		begin
			Insert into Rating(ClientId, TaskId, RatingStar, RatingReview)
			values(@clientId, @taskId, @ratingStar, @ratingReview)
		end
	end
end

Select * from Rating;

--17, Update Rating
Go
Create Procedure UpdateRating
	@ratingId int,
	@ratingStar int,
	@ratingReview varchar(max)
as begin
	if exists(Select 1 from Rating where RatingId = @ratingId)
	begin
		Update Rating
		set RatingStar = @ratingStar,
			RatingReview = @ratingReview
		where RatingId = @ratingId;
	end
end