-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 26 Nov 2024 pada 15.01
-- Versi server: 10.4.32-MariaDB
-- Versi PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `anonchat`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `message`
--

CREATE TABLE `message` (
  `id_msg` int(11) NOT NULL,
  `to_msg` varchar(500) NOT NULL,
  `msg` text NOT NULL,
  `date_msg` varchar(500) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `message`
--

INSERT INTO `message` (`id_msg`, `to_msg`, `msg`, `date_msg`) VALUES
(1, 'Adli', 'Hai Adli sehat sehat ya jaga kesehatan jangan sakit sakit', '2024-11-19 11:22:18'),
(3, 'Naufal', 'Hai Naufal sehat sehat ya jaga kesehatan jangan sakit sakit', '2024-11-19 11:20:34'),
(6, 'wikwok', 'awokaowkaowkaowakwo ajg', '16-11-2024 17:10:37'),
(18, 'Naufal Adli', 'Hai Adli sehat sehat ya jaga kesehatan jangan sakit sakit', '2024-11-21 11:13:50'),
(19, 'Adli', 'Hai Adli sehat sehat ya jaga kesehatan jangan sakit sakit', '2024-11-21 11:13:50'),
(21, 'inisial d', 'semangat ya kakk sehat sehat selalu', '2024-11-23 12:04:26');

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `message`
--
ALTER TABLE `message`
  ADD PRIMARY KEY (`id_msg`);

--
-- AUTO_INCREMENT untuk tabel yang dibuang
--

--
-- AUTO_INCREMENT untuk tabel `message`
--
ALTER TABLE `message`
  MODIFY `id_msg` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
