
create VIEW hotels_barcelona_activities AS
select hotels.id, hotels.name as hotel, city_name as city, country_name as country, activities.name as activity
from hotels
    join cities on cities.id = hotels.city
    join countries on countries.id = cities.country
    join activities on activities.city = cities.id
where city_name = 'barcelona'