CREATE DATABASE euroquest;
CREATE USER 'euroquest'@'localhost' IDENTIFIED BY 'euroquest';
GRANT ALL PRIVILEGES ON euroquest.* TO 'euroquest'@'localhost';