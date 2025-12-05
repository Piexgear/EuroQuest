CREATE DATABASE EuroQuest;
USE EuroQuest;

CREATE TABLE IF NOT EXISTS `user` (
    `id` INTEGER NOT NULL AUTO_INCREMENT,
    `name` VARCHAR(255) NOT NULL,
    `email` VARCHAR(255) NOT NULL UNIQUE,
    `password` VARCHAR(255) NOT NULL,
    `role` ENUM('admin', 'customer') NOT NULL DEFAULT 'customer',
    PRIMARY KEY(`id`)
);

CREATE TABLE IF NOT EXISTS `country` (
    `id` INTEGER NOT NULL AUTO_INCREMENT,
    `country_name` VARCHAR(255) NOT NULL,
    PRIMARY KEY(`id`)
);

CREATE TABLE IF NOT EXISTS `city` (
    `id` INTEGER NOT NULL AUTO_INCREMENT,
    `city_name` VARCHAR(255) NOT NULL,
    `country` INTEGER NOT NULL,
    PRIMARY KEY(`id`)
);

CREATE TABLE IF NOT EXISTS `hotels` (
    `id` INTEGER NOT NULL AUTO_INCREMENT,
    `name` VARCHAR(255) NOT NULL,
    `city` INTEGER NOT NULL,
    `amount_of_rooms` INTEGER NOT NULL,
    `description` TEXT NOT NULL,
    `beach_distance` INTEGER NOT NULL,
    `pool` TINYINT NOT NULL,
    `breakfast` TINYINT NOT NULL,
    `center_distance` INTEGER NOT NULL,
    PRIMARY KEY(`id`)
);

CREATE TABLE IF NOT EXISTS `rooms` (
    `id` INTEGER NOT NULL AUTO_INCREMENT,
    `number` INTEGER NOT NULL,
    `hotel` INTEGER NOT NULL,
    `capacity` INTEGER NOT NULL,
    `price` INTEGER NOT NULL,
    PRIMARY KEY(`id`)
);

CREATE TABLE IF NOT EXISTS `activity` (
    `id` INTEGER NOT NULL AUTO_INCREMENT,
    `name` VARCHAR(255) NOT NULL,
    `duration` INTEGER NOT NULL,
    `price` INTEGER NOT NULL,
    `address` VARCHAR(255) NOT NULL,
    `city` INTEGER NOT NULL,
    `capacity` INTEGER NOT NULL,
    `description` TEXT NOT NULL,
    PRIMARY KEY(`id`)
);

CREATE TABLE IF NOT EXISTS `package` (
    `id` INTEGER NOT NULL AUTO_INCREMENT,
    `hotel` INTEGER NOT NULL,
    PRIMARY KEY(`id`)
);

CREATE TABLE IF NOT EXISTS `package_activity` (
    `package` INTEGER NOT NULL,
    `activity` INTEGER NOT NULL,
    PRIMARY KEY(`package`, `activity`)
);

CREATE TABLE IF NOT EXISTS `bookings` (
    `id` INTEGER NOT NULL AUTO_INCREMENT,
    `package` INTEGER NOT NULL,
    `user` INTEGER NOT NULL,
    `check_in` DATE NOT NULL,
    `check_out` DATE NOT NULL,
    `guests` INTEGER NOT NULL,
    PRIMARY KEY(`id`)
);

CREATE TABLE IF NOT EXISTS `room_booking` (
    `booking` INTEGER NOT NULL,
    `room` INTEGER NOT NULL,
    PRIMARY KEY(`booking`, `room`)
);

ALTER TABLE `city`
    ADD FOREIGN KEY(`country`) REFERENCES `country`(`id`);

ALTER TABLE `hotels`
    ADD FOREIGN KEY(`city`) REFERENCES `city`(`id`);

ALTER TABLE `rooms`
    ADD FOREIGN KEY(`hotel`) REFERENCES `hotels`(`id`);

ALTER TABLE `activity`
    ADD FOREIGN KEY(`city`) REFERENCES `city`(`id`);

ALTER TABLE `package`
    ADD FOREIGN KEY(`hotel`) REFERENCES `hotels`(`id`);

ALTER TABLE `package_activity`
    ADD FOREIGN KEY(`package`) REFERENCES `package`(`id`);

ALTER TABLE `package_activity`
    ADD FOREIGN KEY(`activity`) REFERENCES `activity`(`id`);

ALTER TABLE `bookings`
    ADD FOREIGN KEY(`package`) REFERENCES `package`(`id`);

ALTER TABLE `bookings`
    ADD FOREIGN KEY(`user`) REFERENCES `user`(`id`);

ALTER TABLE `room_booking`
    ADD FOREIGN KEY(`booking`) REFERENCES `bookings`(`id`);

ALTER TABLE `room_booking`
    ADD FOREIGN KEY(`room`) REFERENCES `rooms`(`id`);
