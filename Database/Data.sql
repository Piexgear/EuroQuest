-- === USERS ===
INSERT INTO users (name, email, password, role) VALUES
('David Puscas', 'david@hotmail.com', 'password123', 'customer'),
('Jonathan Lopez', 'jonathan@hotmail.com', 'password123', 'customer'),
('Jenjira Phayakhruea', 'jenjira@ehotmail.com', 'password123', 'customer'),
('Gustav Fransson', 'gustav@hotmail.com', 'password456', 'customer'),
('Lina Hallgergren', 'Lina@hotmail.com', 'password456', 'customer'),
('John Doe', 'john@hotmail.com', 'password456', 'customer'),
('Admin ', 'admin', 'admin', 'admin');


-- === COUNTRIES ===
INSERT INTO countries (country_name) VALUES
('Spain'),
('Italy'),
('France'),
('Germany');


-- === CITIES (4 per land) ===
INSERT INTO cities (city_name, country) VALUES
-- Spain (1)
('Barcelona', 1),
('Madrid', 1),
('Valencia', 1),
('Seville', 1),

-- Italy (2)
('Rome', 2),
('Milan', 2),
('Venice', 2),
('Florence', 2),

-- France (3)
('Paris', 3),
('Nice', 3),
('Lyon', 3),
('Marseille', 3),

-- Germany (4)
('Berlin', 4),
('Munich', 4),
('Hamburg', 4),
('Cologne', 4);

INSERT INTO hotels (name, city, amount_of_rooms, description, beach_distance, pool, breakfast, center_distance) VALUES
-- Barcelona (1)
('Barcelona Central Hotel', 1, 10, 'Hotel in the heart of Barcelona.', 1500, 1, 1, 300),
('Beachside Barcelona Resort', 1, 10, 'Near Barceloneta beach.', 200, 1, 1, 2000),
('Gothic Quarter Inn', 1, 10, 'Located in the Gothic Quarter.', 1800, 0, 1, 150),
('Sagrada View Suites', 1, 10, 'View of Sagrada Familia.', 2500, 1, 0, 700),

-- Madrid (2)
('Madrid Central Plaza Hotel', 2, 10, 'Close to Puerta del Sol.', 9999, 0, 1, 200),
('Gran Via Hotel', 2, 10, 'Main street location.', 9999, 0, 1, 100),
('Retiro Park Lodge', 2, 10, 'Near El Retiro park.', 9999, 1, 0, 600),
('Royal Palace Suites', 2, 10, 'Near Palacio Real.', 9999, 1, 1, 800),

-- Valencia (3)
('Valencia Beach Hotel', 3, 10, 'Near Malvarrosa Beach.', 300, 1, 1, 1500),
('Arts & Science Hotel', 3, 10, 'Next to Ciudad de las Artes.', 1800, 1, 1, 700),
('Valencia Old Town Inn', 3, 10, 'Located in Ciutat Vella.', 2000, 0, 1, 200),
('Orange Blossom Hotel', 3, 10, 'Quiet and central.', 1700, 1, 1, 500),

-- Seville (4)
('Seville Centro Hotel', 4, 10, 'Near Seville Cathedral.', 9999, 0, 1, 250),
('Triana Riverside Hotel', 4, 10, 'Along the Guadalquivir.', 9999, 1, 1, 500),
('Seville Flamenco Inn', 4, 10, 'Cultural-rich location.', 9999, 0, 1, 300),
('Alcazar Garden Hotel', 4, 10, 'Near Real Alcázar.', 9999, 1, 0, 450),

-- Rome (5)
('Rome Colosseum View Hotel', 5, 10, 'View of the Colosseum.', 9999, 0, 1, 400),
('Vatican City Suites', 5, 10, 'Near St. Peter’s Basilica.', 9999, 0, 1, 700),
('Trevi Fountain Hotel', 5, 10, 'Near Trevi Fountain.', 9999, 1, 1, 300),
('Roman Forum Lodge', 5, 10, 'Historic atmosphere.', 9999, 1, 0, 600),

-- Milan (6)
('Milan Fashion District Hotel', 6, 10, 'Near Via Montenapoleone.', 9999, 1, 1, 400),
('Duomo Square Hotel', 6, 10, 'Next to Milan Cathedral.', 9999, 0, 1, 200),
('Sforza Castle Inn', 6, 10, 'Historic castle area.', 9999, 1, 0, 700),
('Milan Central Suites', 6, 10, 'Close to Milano Centrale.', 9999, 0, 1, 300),

-- Venice (7)
('Venice Grand Canal Hotel', 7, 10, 'Overlooking the Grand Canal.', 9999, 0, 1, 200),
('San Marco Square Inn', 7, 10, 'Right at Piazza San Marco.', 9999, 0, 1, 150),
('Rialto Bridge Suites', 7, 10, 'Historic Rialto area.', 9999, 0, 1, 320),
('Lagoon View Resort', 7, 10, 'Waterfront views.', 9999, 1, 1, 400),

-- Florence (8)
('Florence Duomo Hotel', 8, 10, 'Next to the Duomo.', 9999, 0, 1, 300),
('Uffizi Gallery Inn', 8, 10, 'Close to Uffizi.', 9999, 0, 1, 200),
('Ponte Vecchio Hotel', 8, 10, 'Historic bridge area.', 9999, 1, 1, 450),
('Tuscany View Lodge', 8, 10, 'Scenic city views.', 9999, 1, 0, 700),

-- Paris (9)
('Paris Eiffel Tower Hotel', 9, 10, 'View of Eiffel Tower.', 9999, 1, 1, 350),
('Champs-Élysées Suites', 9, 10, 'Luxury avenue location.', 9999, 0, 1, 200),
('Montmartre Artistic Inn', 9, 10, 'Bohemian district.', 9999, 0, 1, 500),
('Seine Riverside Hotel', 9, 10, 'Beautiful river views.', 9999, 1, 0, 450),

-- Nice (10)
('Nice Beach Resort', 10, 10, 'Right on the beach.', 100, 1, 1, 1500),
('Promenade des Anglais Hotel', 10, 10, 'Famous beachfront road.', 100, 1, 1, 1800),
('Nice Old Town Inn', 10, 10, 'Historic quarter.', 1800, 0, 1, 400),
('Azure Coast Lodge', 10, 10, 'Mediterranean views.', 300, 1, 0, 900),

-- Lyon (11)
('Lyon City Center Hotel', 11, 10, 'Located in Presqu’île.', 9999, 0, 1, 200),
('Basilica Fourvière View Hotel', 11, 10, 'Hilltop location.', 9999, 1, 0, 600),
('Lyon Riverside Suites', 11, 10, 'By the Saône river.', 9999, 1, 1, 300),
('Old Lyon Heritage Inn', 11, 10, 'Historic quarter.', 9999, 0, 1, 250),

-- Marseille (12)
('Marseille Port Hotel', 12, 10, 'Near the Old Port.', 300, 1, 1, 200),
('Notre-Dame de la Garde View', 12, 10, 'Hilltop view point.', 9999, 1, 0, 900),
('Marseille Beach Hotel', 12, 10, 'Next to Prado Beach.', 200, 1, 1, 1500),
('Old Town Marseille Inn', 12, 10, 'Historic quarter.', 9999, 0, 1, 400),

-- Berlin (13)
('Berlin Mitte Central Hotel', 13, 10, 'In the Mitte district.', 9999, 0, 1, 250),
('Brandenburg Gate Suites', 13, 10, 'Near the Gate.', 9999, 1, 1, 300),
('Alexanderplatz Inn', 13, 10, 'Central Berlin.', 9999, 0, 1, 150),
('Berlin Wall Memorial Hotel', 13, 10, 'Historic area.', 9999, 1, 0, 700),

-- Munich (14)
('Munich Marienplatz Hotel', 14, 10, 'Right in city center.', 9999, 0, 1, 200),
('English Garden Lodge', 14, 10, 'Near Englischer Garten.', 9999, 1, 1, 600),
('Oktoberfest Hotel', 14, 10, 'Near festival grounds.', 9999, 1, 1, 900),
('Munich Old Town Inn', 14, 10, 'Historic location.', 9999, 0, 1, 400),

-- Hamburg (15)
('Hamburg Harbor Hotel', 15, 10, 'Near port area.', 300, 1, 1, 200),
('Elbphilharmonie View Hotel', 15, 10, 'Waterfront views.', 9999, 1, 0, 800),
('Hamburg City Center Inn', 15, 10, 'Central location.', 9999, 0, 1, 250),
('St. Pauli District Hotel', 15, 10, 'Close to nightlife.', 9999, 1, 1, 550),

-- Cologne (16)
('Cologne Cathedral Hotel', 16, 10, 'Next to Kölner Dom.', 9999, 0, 1, 200),
('Rhine Riverside Inn', 16, 10, 'Beautiful riverfront.', 9999, 1, 0, 600),
('Old Town Cologne Hotel', 16, 10, 'Historic area.', 9999, 0, 1, 300),
('Cologne Central Station Suites', 16, 10, 'Convenient location.', 9999, 1, 1, 150);

INSERT INTO rooms (number, hotels_id, capacity, price) VALUES
(1,1,2,145),(2,1,2,145),(3,1,2,145),(4,1,2,145),(5,1,2,145),
(6,1,2,145),(7,1,2,145),(8,1,2,145),(9,1,2,145),(10,1,2,145),
(1,2,2,150),(2,2,2,150),(3,2,2,150),(4,2,2,150),(5,2,2,150),
(6,2,2,150),(7,2,2,150),(8,2,2,150),(9,2,2,150),(10,2,2,150),
(1,3,2,138),(2,3,2,138),(3,3,2,138),(4,3,2,138),(5,3,2,138),
(6,3,2,138),(7,3,2,138),(8,3,2,138),(9,3,2,138),(10,3,2,138),
(1,4,2,155),(2,4,2,155),(3,4,2,155),(4,4,2,155),(5,4,2,155),
(6,4,2,155),(7,4,2,155),(8,4,2,155),(9,4,2,155),(10,4,2,155),
(1,5,2,120),(2,5,2,120),(3,5,2,120),(4,5,2,120),(5,5,2,120),
(6,5,2,120),(7,5,2,120),(8,5,2,120),(9,5,2,120),(10,5,2,120),
(1,6,2,110),(2,6,2,110),(3,6,2,110),(4,6,2,110),(5,6,2,110),
(6,6,2,110),(7,6,2,110),(8,6,2,110),(9,6,2,110),(10,6,2,110),
(1,7,2,125),(2,7,2,125),(3,7,2,125),(4,7,2,125),(5,7,2,125),
(6,7,2,125),(7,7,2,125),(8,7,2,125),(9,7,2,125),(10,7,2,125),
(1,8,2,118),(2,8,2,118),(3,8,2,118),(4,8,2,118),(5,8,2,118),
(6,8,2,118),(7,8,2,118),(8,8,2,118),(9,8,2,118),(10,8,2,118),
(1,9,2,95),(2,9,2,95),(3,9,2,95),(4,9,2,95),(5,9,2,95),
(6,9,2,95),(7,9,2,95),(8,9,2,95),(9,9,2,95),(10,9,2,95),
(1,10,2,100),(2,10,2,100),(3,10,2,100),(4,10,2,100),(5,10,2,100),
(6,10,2,100),(7,10,2,100),(8,10,2,100),(9,10,2,100),(10,10,2,100),
(1,11,2,88),(2,11,2,88),(3,11,2,88),(4,11,2,88),(5,11,2,88),
(6,11,2,88),(7,11,2,88),(8,11,2,88),(9,11,2,88),(10,11,2,88),
(1,12,2,92),(2,12,2,92),(3,12,2,92),(4,12,2,92),(5,12,2,92),
(6,12,2,92),(7,12,2,92),(8,12,2,92),(9,12,2,92),(10,12,2,92),
(1,13,2,110),(2,13,2,110),(3,13,2,110),(4,13,2,110),(5,13,2,110),
(6,13,2,110),(7,13,2,110),(8,13,2,110),(9,13,2,110),(10,13,2,110),
(1,14,2,125),(2,14,2,125),(3,14,2,125),(4,14,2,125),(5,14,2,125),
(6,14,2,125),(7,14,2,125),(8,14,2,125),(9,14,2,125),(10,14,2,125),
(1,15,2,105),(2,15,2,105),(3,15,2,105),(4,15,2,105),(5,15,2,105),
(6,15,2,105),(7,15,2,105),(8,15,2,105),(9,15,2,105),(10,15,2,105),
(1,16,2,115),(2,16,2,115),(3,16,2,115),(4,16,2,115),(5,16,2,115),
(6,16,2,115),(7,16,2,115),(8,16,2,115),(9,16,2,115),(10,16,2,115),
(1,17,2,130),(2,17,2,130),(3,17,2,130),(4,17,2,130),(5,17,2,130),
(6,17,2,130),(7,17,2,130),(8,17,2,130),(9,17,2,130),(10,17,2,130),
(1,18,2,120),(2,18,2,120),(3,18,2,120),(4,18,2,120),(5,18,2,120),
(6,18,2,120),(7,18,2,120),(8,18,2,120),(9,18,2,120),(10,18,2,120),
(1,19,2,135),(2,19,2,135),(3,19,2,135),(4,19,2,135),(5,19,2,135),
(6,19,2,135),(7,19,2,135),(8,19,2,135),(9,19,2,135),(10,19,2,135),
(1,20,2,125),(2,20,2,125),(3,20,2,125),(4,20,2,125),(5,20,2,125),
(6,20,2,125),(7,20,2,125),(8,20,2,125),(9,20,2,125),(10,20,2,125),
(1,21,2,100),(2,21,2,100),(3,21,2,100),(4,21,2,100),(5,21,2,100),
(6,21,2,100),(7,21,2,100),(8,21,2,100),(9,21,2,100),(10,21,2,100),
(1,22,2,110),(2,22,2,110),(3,22,2,110),(4,22,2,110),(5,22,2,110),
(6,22,2,110),(7,22,2,110),(8,22,2,110),(9,22,2,110),(10,22,2,110),
(1,23,2,105),(2,23,2,105),(3,23,2,105),(4,23,2,105),(5,23,2,105),
(6,23,2,105),(7,23,2,105),(8,23,2,105),(9,23,2,105),(10,23,2,105),
(1,24,2,115),(2,24,2,115),(3,24,2,115),(4,24,2,115),(5,24,2,115),
(6,24,2,115),(7,24,2,115),(8,24,2,115),(9,24,2,115),(10,24,2,115),
(1,25,2,95),(2,25,2,95),(3,25,2,95),(4,25,2,95),(5,25,2,95),
(6,25,2,95),(7,25,2,95),(8,25,2,95),(9,25,2,95),(10,25,2,95),
(1,26,2,105),(2,26,2,105),(3,26,2,105),(4,26,2,105),(5,26,2,105),
(6,26,2,105),(7,26,2,105),(8,26,2,105),(9,26,2,105),(10,26,2,105),
(1,27,2,110),(2,27,2,110),(3,27,2,110),(4,27,2,110),(5,27,2,110),
(6,27,2,110),(7,27,2,110),(8,27,2,110),(9,27,2,110),(10,27,2,110),
(1,28,2,100),(2,28,2,100),(3,28,2,100),(4,28,2,100),(5,28,2,100),
(6,28,2,100),(7,28,2,100),(8,28,2,100),(9,28,2,100),(10,28,2,100),
(1,29,2,120),(2,29,2,120),(3,29,2,120),(4,29,2,120),(5,29,2,120),
(6,29,2,120),(7,29,2,120),(8,29,2,120),(9,29,2,120),(10,29,2,120),
(1,30,2,130),(2,30,2,130),(3,30,2,130),(4,30,2,130),(5,30,2,130),
(6,30,2,130),(7,30,2,130),(8,30,2,130),(9,30,2,130),(10,30,2,130),
(1,31,2,115),(2,31,2,115),(3,31,2,115),(4,31,2,115),(5,31,2,115),
(6,31,2,115),(7,31,2,115),(8,31,2,115),(9,31,2,115),(10,31,2,115),
(1,32,2,125),(2,32,2,125),(3,32,2,125),(4,32,2,125),(5,32,2,125),
(6,32,2,125),(7,32,2,125),(8,32,2,125),(9,32,2,125),(10,32,2,125),
(1,33,2,110),(2,33,2,110),(3,33,2,110),(4,33,2,110),(5,33,2,110),
(6,33,2,110),(7,33,2,110),(8,33,2,110),(9,33,2,110),(10,33,2,110),
(1,34,2,115),(2,34,2,115),(3,34,2,115),(4,34,2,115),(5,34,2,115),
(6,34,2,115),(7,34,2,115),(8,34,2,115),(9,34,2,115),(10,34,2,115),
(1,35,2,120),(2,35,2,120),(3,35,2,120),(4,35,2,120),(5,35,2,120),
(6,35,2,120),(7,35,2,120),(8,35,2,120),(9,35,2,120),(10,35,2,120),
(1,36,2,130),(2,36,2,130),(3,36,2,130),(4,36,2,130),(5,36,2,130),
(6,36,2,130),(7,36,2,130),(8,36,2,130),(9,36,2,130),(10,36,2,130),
(1,37,2,125),(2,37,2,125),(3,37,2,125),(4,37,2,125),(5,37,2,125),
(6,37,2,125),(7,37,2,125),(8,37,2,125),(9,37,2,125),(10,37,2,125),
(1,38,2,135),(2,38,2,135),(3,38,2,135),(4,38,2,135),(5,38,2,135),
(6,38,2,135),(7,38,2,135),(8,38,2,135),(9,38,2,135),(10,38,2,135),
(1,39,2,140),(2,39,2,140),(3,39,2,140),(4,39,2,140),(5,39,2,140),
(6,39,2,140),(7,39,2,140),(8,39,2,140),(9,39,2,140),(10,39,2,140),
(1,40,2,145),(2,40,2,145),(3,40,2,145),(4,40,2,145),(5,40,2,145),
(6,40,2,145),(7,40,2,145),(8,40,2,145),(9,40,2,145),(10,40,2,145),
(1,41,2,110),(2,41,2,110),(3,41,2,110),(4,41,2,110),(5,41,2,110),
(6,41,2,110),(7,41,2,110),(8,41,2,110),(9,41,2,110),(10,41,2,110),
(1,42,2,115),(2,42,2,115),(3,42,2,115),(4,42,2,115),(5,42,2,115),
(6,42,2,115),(7,42,2,115),(8,42,2,115),(9,42,2,115),(10,42,2,115),
(1,43,2,125),(2,43,2,125),(3,43,2,125),(4,43,2,125),(5,43,2,125),
(6,43,2,125),(7,43,2,125),(8,43,2,125),(9,43,2,125),(10,43,2,125),
(1,44,2,130),(2,44,2,130),(3,44,2,130),(4,44,2,130),(5,44,2,130),
(6,44,2,130),(7,44,2,130),(8,44,2,130),(9,44,2,130),(10,44,2,130),
(1,45,2,135),(2,45,2,135),(3,45,2,135),(4,45,2,135),(5,45,2,135),
(6,45,2,135),(7,45,2,135),(8,45,2,135),(9,45,2,135),(10,45,2,135),
(1,46,2,140),(2,46,2,140),(3,46,2,140),(4,46,2,140),(5,46,2,140),
(6,46,2,140),(7,46,2,140),(8,46,2,140),(9,46,2,140),(10,46,2,140),
(1,47,2,120),(2,47,2,120),(3,47,2,120),(4,47,2,120),(5,47,2,120),
(6,47,2,120),(7,47,2,120),(8,47,2,120),(9,47,2,120),(10,47,2,120),
(1,48,2,125),(2,48,2,125),(3,48,2,125),(4,48,2,125),(5,48,2,125),
(6,48,2,125),(7,48,2,125),(8,48,2,125),(9,48,2,125),(10,48,2,125),
(1,49,2,130),(2,49,2,130),(3,49,2,130),(4,49,2,130),(5,49,2,130),
(6,49,2,130),(7,49,2,130),(8,49,2,130),(9,49,2,130),(10,49,2,130),
(1,50,2,135),(2,50,2,135),(3,50,2,135),(4,50,2,135),(5,50,2,135),
(6,50,2,135),(7,50,2,135),(8,50,2,135),(9,50,2,135),(10,50,2,135),
(1,51,2,110),(2,51,2,110),(3,51,2,110),(4,51,2,110),(5,51,2,110),
(6,51,2,110),(7,51,2,110),(8,51,2,110),(9,51,2,110),(10,51,2,110),
(1,52,2,115),(2,52,2,115),(3,52,2,115),(4,52,2,115),(5,52,2,115),
(6,52,2,115),(7,52,2,115),(8,52,2,115),(9,52,2,115),(10,52,2,115),
(1,53,2,120),(2,53,2,120),(3,53,2,120),(4,53,2,120),(5,53,2,120),
(6,53,2,120),(7,53,2,120),(8,53,2,120),(9,53,2,120),(10,53,2,120),
(1,54,2,130),(2,54,2,130),(3,54,2,130),(4,54,2,130),(5,54,2,130),
(6,54,2,130),(7,54,2,130),(8,54,2,130),(9,54,2,130),(10,54,2,130),
(1,55,2,135),(2,55,2,135),(3,55,2,135),(4,55,2,135),(5,55,2,135),
(6,55,2,135),(7,55,2,135),(8,55,2,135),(9,55,2,135),(10,55,2,135),
(1,56,2,140),(2,56,2,140),(3,56,2,140),(4,56,2,140),(5,56,2,140),
(6,56,2,140),(7,56,2,140),(8,56,2,140),(9,56,2,140),(10,56,2,140),
(1,57,2,125),(2,57,2,125),(3,57,2,125),(4,57,2,125),(5,57,2,125),
(6,57,2,125),(7,57,2,125),(8,57,2,125),(9,57,2,125),(10,57,2,125),
(1,58,2,130),(2,58,2,130),(3,58,2,130),(4,58,2,130),(5,58,2,130),
(6,58,2,130),(7,58,2,130),(8,58,2,130),(9,58,2,130),(10,58,2,130),
(1,59,2,110),(2,59,2,110),(3,59,2,110),(4,59,2,110),(5,59,2,110),
(6,59,2,110),(7,59,2,110),(8,59,2,110),(9,59,2,130),(10,59,2,130);


INSERT INTO activities (name, duration, price, address, city, capacity, description) VALUES
('Guided Tour: Sagrada Familia', 120, 40, 'Carrer de Mallorca', 1, 20, 'Guidad tur i Gaudis mästerverk.'),
('La Rambla Walking Tour', 90, 20, 'La Rambla', 1, 25, 'Stadsrundtur i Barcelonas hjärta.'),
('Beach Yoga Barceloneta', 60, 25, 'Barceloneta Beach', 1, 30, 'Yoga vid stranden.'),
('Tapas Tasting Experience', 120, 50, 'Gothic Quarter', 1, 15, 'Lokala tapas och vinprovning.'),
('Camp Nou Stadium Tour', 150, 35, 'Carrer d\Aristides Maillol', 1, 40, 'FC Barcelonas ikoniska arena.'),
('Park Güell Tour', 90, 25, 'Park Güell', 1, 20, 'Upptäck Gaudis färgsprakande park.'),
('Montjuïc Cable Car Ride', 30, 15, 'Montjuïc', 1, 50, 'Utsikt över Barcelona.'),
('Barcelona Aquarium Visit', 60, 28, 'Port Vell', 1, 60, 'Populärt akvarium för alla åldrar.'),
('Paella Cooking Class', 180, 55, 'El Born', 1, 12, 'Lär dig laga äkta paella.'),
('Sunset Sailing Tour', 120, 70, 'Port Olímpic', 1, 8, 'Segling längs Barcelonas kust.'),
('Royal Palace Guided Tour', 120, 35, 'Calle de Bailén', 2, 25, 'Guidad tur i Madrids kungliga palats.'),
('Retiro Park Boat Ride', 60, 15, 'Parque del Retiro', 2, 20, 'Ro en båt i den berömda parken.'),
('Prado Museum Tour', 120, 30, 'Paseo del Prado', 2, 20, 'Kulturupplevelse i Spaniens mest kända museum.'),
('Tapas Evening in La Latina', 150, 45, 'Calle Cava Baja', 2, 15, 'Tapasrunda i Madrids gastronomiska kvarter.'),
('Oceanogràfic Visit', 120, 32, 'Ciutat de les Arts i les Ciències', 3, 50, 'Besök Europas största akvarium.'),
('Old Town Bike Tour', 90, 20, 'Plaça de la Reina', 3, 20, 'Cykeltur genom Valencias historiska centrum.'),
('Paella Cooking Class', 150, 55, 'Carrer del Mar', 3, 12, 'Lär dig laga originalpaellan från Valencia.'),
('Beach Volleyball Session', 60, 10, 'Playa de la Malvarrosa', 3, 30, 'Volleyboll på stranden.'),
('Seville Cathedral Tour', 90, 25, 'Av. de la Constitución', 4, 20, 'Besök världens största gotiska katedral.'),
('Flamenco Night Show', 60, 35, 'Barrio de Triana', 4, 80, 'Autentisk flamencoshow.'),
('Alcázar Palace Tour', 120, 30, 'Patio de Banderas', 4, 25, 'Upptäck Seville Alcázars palats och trädgårdar.'),
('Guadalquivir River Cruise', 75, 18, 'Torre del Oro', 4, 50, 'Båtutflykt längs floden.'),
('Colosseum & Forum Guided Tour', 180, 55, 'Piazza del Colosseo', 5, 25, 'Historisk rundtur i antika Rom.'),
('Vatican Museums Tour', 150, 45, 'Viale Vaticano', 5, 20, 'Besök Sixtinska kapellet och Vatikanmuseerna.'),
('Trastevere Food Tour', 120, 50, 'Trastevere', 5, 15, 'Smaka italienska delikatesser.'),
('Trevi Fountain Photo Walk', 60, 15, 'Piazza di Trevi', 5, 30, 'Guidad fototur.'),
('Duomo Rooftop Tour', 90, 28, 'Piazza del Duomo', 6, 20, 'Besök katedralens tak med panoramavy.'),
('Fashion District Shopping Walk', 120, 0, 'Via Montenapoleone', 6, 15, 'Guidad modevandring.'),
('Sforza Castle Museum Tour', 120, 25, 'Piazza Castello', 6, 25, 'Historisk rundtur i slottet.'),
('Milan Aperitivo Experience', 90, 30, 'Navigli District', 6, 20, 'Italiensk aperitivo vid kanalerna.'),
('Gondola Ride', 30, 80, 'Grand Canal', 7, 6, 'Klassisk gondoltur genom kanalerna.'),
('St. Mark’s Basilica Tour', 90, 30, 'Piazza San Marco', 7, 20, 'Guidad tur i den ikoniska basilikan.'),
('Murano Glassmaking Workshop', 120, 45, 'Isola di Murano', 7, 12, 'Lär dig glasblåsningens konst.'),
('Rialto Market Food Tour', 120, 40, 'Rialto Market', 7, 15, 'Kulinarisk rundtur genom marknaden.'),
('Uffizi Gallery Tour', 150, 35, 'Piazzale degli Uffizi', 8, 20, 'Guidad tur genom världsberömt museum.'),
('Duomo Dome Climb', 90, 25, 'Piazza del Duomo', 8, 20, 'Klättra upp i Brunelleschis kupol.'),
('Tuscany Wine Tasting', 180, 60, 'Chianti Region', 8, 20, 'Vinprovning på vingård utanför Florens.'),
('Ponte Vecchio Walking Tour', 60, 15, 'Ponte Vecchio', 8, 30, 'Historisk rundtur över bron.'),
('Eiffel Tower Summit Tour', 120, 45, 'Champ de Mars', 9, 25, 'Guidad tur till toppen av Eiffeltornet.'),
('Seine River Cruise', 60, 18, 'Port de la Bourdonnais', 9, 100, 'Avkopplande båttur på Seine.'),
('Louvre Museum Tour', 150, 40, 'Rue de Rivoli', 9, 20, 'Rundtur i världens största museum.'),
('Montmartre Art Walk', 90, 20, 'Montmartre', 9, 20, 'Promenad i konstnärskvarteren.'),
('Old Town Walking Tour', 90, 22, 'Vieux Nice', 10, 20, 'Guidad tur i den historiska stadsdelen.'),
('Beach Kayaking', 60, 25, 'Promenade des Anglais', 10, 12, 'Kajakpaddling längs kusten.'),
('Perfume Workshop', 120, 55, 'Grasse Factory', 10, 10, 'Skapa din egen parfym.'),
('Sunset Coast Hike', 120, 15, 'Cap de Nice', 10, 25, 'Vacker vandring längs klipporna.'),
('Fourvière Hill Tour', 90, 20, 'Basilique Notre-Dame de Fourvière', 11, 25, 'Historisk utsiktstur.'),
('Lyon Gourmet Food Tour', 120, 45, 'Vieux Lyon', 11, 15, 'Provsmakning av lokala specialiteter.'),
('Saône River Boat Tour', 75, 18, 'Quai de Saône', 11, 60, 'Båtutflykt genom Lyon.'),
('Silk Weaving Workshop', 120, 40, 'Croix-Rousse', 11, 10, 'Lär dig traditionellt sidenhantverk.'),
('Old Port Walking Tour', 90, 20, 'Vieux-Port', 12, 20, 'Historisk rundtur vid hamnen.'),
('Calanques Boat Trip', 180, 55, 'Port de Marseille', 12, 60, 'Båttur till Calanques nationalpark.'),
('Notre-Dame de la Garde Visit', 60, 12, 'Rue Fort du Sanctuaire', 12, 40, 'Besök Marseilles ikoniska basilika.'),
('Local Seafood Tasting', 120, 48, 'Le Panier', 12, 15, 'Provsmakning av färska skaldjur.'),
('Berlin Wall History Tour', 120, 25, 'Bernauer Strasse', 13, 25, 'Guidad tur kring Berlinmurens historia.'),
('Museum Island Pass', 180, 40, 'Museum Island', 13, 50, 'Tillträde till flera museer.'),
('Brandenburg Gate Night Walk', 60, 12, 'Pariser Platz', 13, 30, 'Kvällspromenad genom historiskt område.'),
('Spree River Cruise', 75, 18, 'Schiffbauerdamm', 13, 80, 'Båtutflykt genom centrala Berlin.'),
('Marienplatz Walking Tour', 90, 20, 'Marienplatz', 14, 25, 'Guidad stadsvandring.'),
('Bavarian Beer Tasting', 120, 45, 'Hofbräuhaus', 14, 30, 'Smaka traditionella bayerska ölsorter.'),
('English Garden Bike Ride', 120, 25, 'Englischer Garten', 14, 20, 'Cykeltur i stadens stora park.'),
('Nymphenburg Palace Tour', 150, 30, 'Schloss Nymphenburg', 14, 20, 'Besök i storslaget palats.'),
('Harbor Boat Tour', 75, 20, 'Landungsbrücken', 15, 100, 'Populär rundtur i hamnen.'),
('Miniatur Wunderland Visit', 120, 28, 'Kehrwieder', 15, 60, 'Världens största modelljärnväg.'),
('St. Pauli Nightlife Tour', 120, 30, 'Reeperbahn', 15, 30, 'Guidad rundtur i nöjesdistriktet.'),
('Elbphilharmonie Plaza Visit', 60, 12, 'Platz der Deutschen Einheit', 15, 50, 'Utsikt från ikoniska konsertbyggnaden.'),
('Cologne Cathedral Tour', 90, 20, 'Domkloster', 16, 25, 'Guidad tur i den berömda katedralen.'),
('Rhine River Cruise', 75, 18, 'Konrad-Adenauer-Ufer', 16, 80, 'Båttur längs Rhen.'),
('Chocolate Museum Tour', 120, 22, 'Am Schokoladenmuseum', 16, 30, 'Besök chokladmuseum med provsmakning.'),
('Old Town Beer Walk', 120, 35, 'Altstadt', 16, 20, 'Rundtur med lokala ölsorter.');














