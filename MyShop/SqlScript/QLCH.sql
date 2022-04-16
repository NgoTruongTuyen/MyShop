create database QLCH;
go
use QLCH;
go

create table Product(
	productId int identity(1,1) primary key,
	productName nvarchar(100),
	imageURL varchar(50),
	stock int,
	costPrice int,
	sellingPrice int,
	brand varchar(15),
	screenSize float,
	os varchar(15),
	color varchar(15),
	memory int,
	storage int,
	battery int,
	releaseDate datetime,
	buyCounts int,
	viewCounts int
);

create table Discount(
	discountID int identity(1,1) primary key,
	productID int,
	percent int,
	startDate datetime,
	endDate datetime
);

create table Order(
	orderID int identity(1,1) primary key,
	date datetime,
	customer_name varchar(50),
	customer_phone varchar(15),
	customer_address varchar(50),
	totalAmount int,
	totalPrice int,
);

create table Order_Product(
	orderID int,
	productID int,
	price int,
	amount int,
	CONSTRAINT PK_Order PRIMARY KEY (orderID,productID)
);

create table User(
	username varchar(50) primary key,
	password varchar(50),
);

insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('Iphone 13 Pro Max','a',100,29990000, 27990000,'Apple',6.7,'Ios','Yellow',6,128,4352,cast('2022-01-21' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('Iphone 13 Pro','a',100,27990000, 25990000,'Apple',6.1,'Ios','Blue',6,128,4352,cast('2021-10-17' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('Iphone 13','a',100,21490000, 20000000,'Apple',6.1,'Ios','Pink',6,128,3225,cast('2021-09-01' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('Iphone 11','a',100,18990000, 17990000,'Apple',6.1,'Ios','Black',6,128,2122,cast('2019-03-11' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('Iphone 13 Mini','a',100,18990000, 16990000,'Apple',5.4,'Ios','Blue',4,128,2438,cast('2021-10-10' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('Iphone SE','a',100,10690000, 9990000,'Apple',4.7,'Ios','White',3,64,1821,cast('2018-07-22' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('Iphone 12 Pro Max','a',100,29490000, 25990000,'Apple',6.7,'Ios','Yellow',6,256,3687,cast('2020-03-11' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('Iphone 12 Mini','a',100,16190000, 15990000,'Apple',5.4,'Ios','Black',4,64,2227,cast('2021-04-19' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('Iphone XR','a',100,13490000, 12990000,'Apple',6.1,'Ios','White',3,128,2942,cast('2019-11-03' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('Iphone 12 Pro','a',100,24590000, 22000000,'Apple',6.7,'Ios','Yellow',6,256,2815,cast('2020-12-12' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('Iphone 7','a',100,4590000, 3900000,'Apple',4.7,'Ios','Yellow',2,32,1960,cast('2018-06-09' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('Iphone 7 Plus','a',100,4790000, 4090000,'Apple',5.5,'Ios','White',3,32,2900,cast('2018-10-22' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('Iphone 6S','a',100,2990000, 1799000,'Apple',4.7,'Ios','Gray',2,64,1715,cast('208-08-21' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('Iphone 6S Plus','a',100,3990000, 17990000,'Apple',5.1,'Ios','Gray',2,64,2750,cast('2019-09-21' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('Iphone 6','a',100,2390000, 17990000,'Apple',4.7,'Ios','Yellow',1,32,1810,cast('2021-08-21' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('Samsung Galaxy S22 Ultra','a',100,30990000, 29990000,'Samsung',6.8,'Android','Red',8,128,5000,cast('2022-03-21' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('Samsung Galaxy A52s','a',100,8990000, 7990000,'Samsung',6.5,'Android','Green',8,128,4500,cast('2020-06-27' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('Samsung Galaxy A03','a',100,2890000, 2500000,'Samsung',6.5,'Android','Blue',3,32,5000,cast('2018-05-11' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('Samsung Galaxy Z Fold3','a',100,36990000, 35990000,'Samsung',7.6,'Android','Black',12,256,4400,cast('2018-05-11' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('OPPO Reno7 Z','a',100,10490000, 9990000,'OPPO',6.43,'Android','Black',8,128,4500,cast('2021-11-13' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('OPPO Reno6','a',100,12990000, 10990000,'OPPO',6.43,'Android','Black',8,128,4300,cast('2020-12-23' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('Xiaomi 11T','a',100,10990000, 9990000,'Xiaomi',6.67,'Android','White',8,128,5000,cast('2021-09-23' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('Vivo Y15s','a',100,3490000, 3090000,'Vivo',6.51,'Android','Blue',3,32,5000,cast('2021-08-08' as datetime),0,0)
insert into Product (productName, imageURL, stock, costPrice, sellingPrice, brand, screenSize, os, color, memory, storage, battery, releaseDate, buyCounts, viewCounts)
values('Realme C35','a',100,3990000, 3090000,'Realme',6.6,'Android','Green',4,64,5000,cast('2022-10-08' as datetime),0,0)