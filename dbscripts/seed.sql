\connect municipalitydb
CREATE TABLE municipality
(
 id serial PRIMARY KEY,
 municipality_name VARCHAR (50) NOT NULL,
 period_start DATE NOT NULL,
 period_end DATE NOT NULL,
 yearly FLOAT,
 monthly FLOAT,
 weekly FLOAT,
 daily FLOAT

);
ALTER TABLE "municipality" OWNER TO municipalityuser;
Insert into municipality(municipality_name,period_start,period_end,yearly) values('Copenhagen','2016-01-01','2016-12-31', 0.2);
Insert into municipality(municipality_name,period_start,period_end,monthly) values('Copenhagen','2016-05-01','2016-05-31', 0.4);
Insert into municipality(municipality_name,period_start,period_end,daily) values('Copenhagen','2016-01-01','2016-01-01', 0.1);
Insert into municipality(municipality_name,period_start,period_end,daily) values('Copenhagen','2016-12-25','2016-12-25', 0.1);