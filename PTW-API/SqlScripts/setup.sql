CREATE DATABASE ptw;
CREATE DATABASE ptw_jobs;

CREATE TABLE ptw.forecast (
	id int auto_increment not null primary key,
	created_on datetime not null,
	deleted_on datetime null,
	forecast_date date not null,
	weather_main varchar(50) not null,
	weather_description varchar(50) null,
	weather_icon varchar(10) null,
	temperature_in_celsius int not null,
	city_name varchar(100) not null,
	city_country_code varchar(5) not null
);

CREATE INDEX idx_date
ON ptw.forecasts(forecast_date);