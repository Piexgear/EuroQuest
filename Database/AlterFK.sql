-- country_id - country
ALTER TABLE city DROP FOREIGN KEY city_ibfk_1;

ALTER TABLE city CHANGE country_id country INT NOT NULL;

ALTER TABLE city
ADD FOREIGN KEY (country) REFERENCES country(id);

-- city_id - city
ALTER TABLE hotels DROP FOREIGN KEY hotels_ibfk_1;

ALTER TABLE hotels CHANGE city_id city INT NOT NULL;

ALTER TABLE hotels
ADD FOREIGN KEY (city) REFERENCES city(id);


-- hotels_id - hotel (Rooms)
ALTER TABLE rooms DROP FOREIGN KEY rooms_ibfk_1;

ALTER TABLE rooms CHANGE hotels_id hotel INT NOT NULL;

ALTER TABLE rooms
ADD FOREIGN KEY (hotel) REFERENCES hotels(id);


-- city_id - city
ALTER TABLE activity DROP FOREIGN KEY activity_ibfk_1;

ALTER TABLE activity CHANGE city_id city INT NOT NULL;

ALTER TABLE activity
ADD FOREIGN KEY (city) REFERENCES city(id);


-- hotel_id - hotel (Package)
ALTER TABLE package DROP FOREIGN KEY package_ibfk_1;

ALTER TABLE package CHANGE hotel_id hotel INT NOT NULL;

ALTER TABLE package
ADD FOREIGN KEY (hotel) REFERENCES hotels(id);


-- (package_id - package) (activity_id - activity) package_activity
ALTER TABLE package_activity DROP FOREIGN KEY package_activity_ibfk_1;
ALTER TABLE package_activity DROP FOREIGN KEY package_activity_ibfk_2;

ALTER TABLE package_activity CHANGE package_id package INT NOT NULL;
ALTER TABLE package_activity CHANGE activity_id activity INT NOT NULL;

ALTER TABLE package_activity
ADD FOREIGN KEY (package) REFERENCES package(id);

ALTER TABLE package_activity
ADD FOREIGN KEY (activity) REFERENCES activity(id);


-- (package_id - package) (user_id - user) bookings
ALTER TABLE bookings DROP FOREIGN KEY bookings_ibfk_1;
ALTER TABLE bookings DROP FOREIGN KEY bookings_ibfk_2;

ALTER TABLE bookings CHANGE package_id package INT NOT NULL;
ALTER TABLE bookings CHANGE user_id user INT NOT NULL;

ALTER TABLE bookings
ADD FOREIGN KEY (package) REFERENCES package(id);

ALTER TABLE bookings
ADD FOREIGN KEY (user) REFERENCES user(id);


-- (booking_id - booking) (room_id - room) room_booking
ALTER TABLE room_booking DROP FOREIGN KEY room_booking_ibfk_1;
ALTER TABLE room_booking DROP FOREIGN KEY room_booking_ibfk_2;

ALTER TABLE room_booking CHANGE booking_id booking INT NOT NULL;
ALTER TABLE room_booking CHANGE room_id room INT NOT NULL;

ALTER TABLE room_booking
ADD FOREIGN KEY (booking) REFERENCES bookings(id);

ALTER TABLE room_booking
ADD FOREIGN KEY (room) REFERENCES rooms(id);
