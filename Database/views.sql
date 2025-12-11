CREATE VIEW hotels_barcelona AS
select hotels.id, hotels.name as hotel, city_name as city, country_name as country
from hotels
    join cities on cities.id = hotels.city
    join countries on countries.id = cities.country
where city_name = 'barcelona'
;

CREATE VIEW hotels_madrid AS
select hotels.id, hotels.name as hotel, city_name as city, country_name as country
from hotels
    join cities on cities.id = hotels.city
    join countries on countries.id = cities.country
where city_name = 'madrid'
;

create view hotels_valencia AS
select hotels.id, hotels.name as hotel, city_name as city, country_name as country
from hotels
    join cities on cities.id = hotels.city
    join countries on countries.id = cities.country
where city_name = 'valencia'
;

create view hotels_seville AS
select hotels.id, hotels.name as hotel, city_name as city, country_name as country
from hotels
    join cities on cities.id = hotels.city
    join countries on countries.id = cities.country
where city_name = 'seville'
;

create view hotels_rome AS
select hotels.id, hotels.name as hotel, city_name as city, country_name as country
from hotels
    join cities on cities.id = hotels.city
    join countries on countries.id = cities.country
where city_name = 'rome'
;

create view hotels_milan AS
select hotels.id, hotels.name as hotel, city_name as city, country_name as country
from hotels
    join cities on cities.id = hotels.city
    join countries on countries.id = cities.country
where city_name = 'milan'
;

create view hotels_venice AS
select hotels.id, hotels.name as hotel, city_name as city, country_name as country
from hotels
    join cities on cities.id = hotels.city
    join countries on countries.id = cities.country
where city_name = 'venice'
;

create view hotels_florence AS
select hotels.id, hotels.name as hotel, city_name as city, country_name as country
from hotels
    join cities on cities.id = hotels.city
    join countries on countries.id = cities.country
where city_name = 'florence'
;

create view hotels_paris AS
select hotels.id, hotels.name as hotel, city_name as city, country_name as country
from hotels
    join cities on cities.id = hotels.city
    join countries on countries.id = cities.country
where city_name = 'paris'
;

create view hotels_nice AS
select hotels.id, hotels.name as hotel, city_name as city, country_name as country
from hotels
    join cities on cities.id = hotels.city
    join countries on countries.id = cities.country
where city_name = 'nice'
;

create view hotels_marseille AS
select hotels.id, hotels.name as hotel, city_name as city, country_name as country
from hotels
    join cities on cities.id = hotels.city
    join countries on countries.id = cities.country
where city_name = 'marseille'
;

create view hotels_berlin AS
select hotels.id, hotels.name as hotel, city_name as city, country_name as country
from hotels
    join cities on cities.id = hotels.city
    join countries on countries.id = cities.country
where city_name = 'berlin'
;

create view hotels_hambrug AS
select hotels.id, hotels.name as hotel, city_name as city, country_name as country
from hotels
    join cities on cities.id = hotels.city
    join countries on countries.id = cities.country
where city_name = 'hamburg'
;

create view hotels_munich AS
select hotels.id, hotels.name as hotel, city_name as city, country_name as country
from hotels
    join cities on cities.id = hotels.city
    join countries on countries.id = cities.country
where city_name = 'munich'
;

create view hotels_cologne AS
select hotels.id, hotels.name as hotel, city_name as city, country_name as country
from hotels
    join cities on cities.id = hotels.city
    join countries on countries.id = cities.country
where city_name = 'cologne'
;



