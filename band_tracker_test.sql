-- phpMyAdmin SQL Dump
-- version 4.7.3
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Nov 04, 2017 at 12:58 AM
-- Server version: 5.6.35
-- PHP Version: 7.1.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `band_tracker_test`
--
CREATE DATABASE IF NOT EXISTS `band_tracker_test` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `band_tracker_test`;

-- --------------------------------------------------------

--
-- Table structure for table `bands`
--

CREATE TABLE `bands` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `genre` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `bands_venues`
--

CREATE TABLE `bands_venues` (
  `id` int(11) NOT NULL,
  `band_id` int(11) NOT NULL,
  `venue_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `bands_venues`
--

INSERT INTO `bands_venues` (`id`, `band_id`, `venue_id`) VALUES
(1, 1, 6),
(2, 3, 7),
(3, 4, 7),
(4, 5, 10),
(5, 6, 10),
(6, 7, 11),
(7, 8, 11),
(8, 9, 14),
(9, 11, 15),
(10, 12, 15),
(11, 13, 18),
(12, 15, 19),
(13, 16, 19),
(14, 17, 22),
(15, 19, 23),
(16, 20, 23),
(17, 21, 26),
(18, 23, 27),
(19, 24, 27),
(20, 25, 30),
(21, 27, 31),
(22, 28, 31),
(23, 29, 34),
(24, 30, 34),
(25, 31, 37),
(26, 32, 37),
(27, 33, 40),
(28, 35, 41),
(29, 36, 41),
(30, 37, 44),
(31, 39, 45),
(32, 40, 45),
(33, 41, 48),
(34, 43, 49),
(35, 44, 49),
(36, 45, 50),
(37, 46, 50),
(38, 47, 53),
(39, 49, 54),
(40, 50, 54),
(43, 53, 58),
(44, 55, 59),
(45, 56, 59),
(49, 60, 64),
(50, 62, 65),
(51, 63, 65),
(52, 64, 66),
(53, 65, 66);

-- --------------------------------------------------------

--
-- Table structure for table `venues`
--

CREATE TABLE `venues` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `address` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `bands`
--
ALTER TABLE `bands`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `bands_venues`
--
ALTER TABLE `bands_venues`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `venues`
--
ALTER TABLE `venues`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `bands`
--
ALTER TABLE `bands`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=66;
--
-- AUTO_INCREMENT for table `bands_venues`
--
ALTER TABLE `bands_venues`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=54;
--
-- AUTO_INCREMENT for table `venues`
--
ALTER TABLE `venues`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=67;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
