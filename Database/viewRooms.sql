create view rooms_in_hotels as
SELECT
    h.id AS hotel_id,
    h.name AS hotel,
    r.id AS room_id,
    r.number AS room_number
FROM hotels h
JOIN rooms r ON r.hotels_id = h.id;
