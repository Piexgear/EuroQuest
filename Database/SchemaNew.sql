CREATE DATABASE EuroQuest;
USE EuroQuest;

CREATE TABLE IF NOT EXISTS `users` (
    `id` INTEGER NOT NULL AUTO_INCREMENT,
    `name` VARCHAR(255) NOT NULL,
    `email` VARCHAR(255) NOT NULL UNIQUE,
    `password` VARCHAR(255) NOT NULL,
    `role` ENUM('admin', 'customer') NOT NULL DEFAULT 'customer',
    PRIMARY KEY(`id`)
);

CREATE TABLE IF NOT EXISTS `countries` (
    `id` INTEGER NOT NULL AUTO_INCREMENT,
    `country_name` VARCHAR(255) NOT NULL,
    PRIMARY KEY(`id`)
);

CREATE TABLE IF NOT EXISTS `cities` (
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

CREATE TABLE IF NOT EXISTS `activities` (
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

CREATE TABLE IF NOT EXISTS `packages` (
    `id` INT NOT NULL AUTO_INCREMENT,
    `hotel` INT NOT NULL,
    `created_by` INT,
    PRIMARY KEY (`id`),
    CONSTRAINT fk_packages_created_by
    FOREIGN KEY (`created_by`) REFERENCES `users`(`id`) 
);

CREATE TABLE IF NOT EXISTS `package_activities` (
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

CREATE TABLE IF NOT EXISTS `room_bookings` (
    `booking` INTEGER NOT NULL,
    `room` INTEGER NOT NULL,
    PRIMARY KEY(`booking`, `room`)
);


ALTER TABLE `cities`
    ADD FOREIGN KEY(`country`) REFERENCES `countries`(`id`);

ALTER TABLE `hotels`
    ADD FOREIGN KEY(`city`) REFERENCES `cities`(`id`);

ALTER TABLE `rooms`
    ADD FOREIGN KEY(`hotel`) REFERENCES `hotels`(`id`);

ALTER TABLE `activities`
    ADD FOREIGN KEY(`city`) REFERENCES `cities`(`id`);

ALTER TABLE `packages`
    ADD FOREIGN KEY(`hotel`) REFERENCES `hotels`(`id`);

ALTER TABLE `package_activities`
    ADD FOREIGN KEY(`package`) REFERENCES `packages`(`id`);

ALTER TABLE `package_activities`
    ADD FOREIGN KEY(`activity`) REFERENCES `activities`(`id`);

ALTER TABLE `bookings`
    ADD FOREIGN KEY(`package`) REFERENCES `packages`(`id`);

ALTER TABLE `bookings`
    ADD FOREIGN KEY(`user`) REFERENCES `users`(`id`);

ALTER TABLE `room_bookings`
    ADD FOREIGN KEY(`booking`) REFERENCES `bookings`(`id`);

ALTER TABLE `room_bookings`
    ADD FOREIGN KEY(`room`) REFERENCES `rooms`(`id`);
