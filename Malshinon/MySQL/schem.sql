CREATE DATABASE malshinon CHARACTER SET utf8 COLLATE utf8_general_ci;

CREATE TABLE People
(
    id INT PRIMARY KEY AUTO_INCREMENT,
    first_name VARCHAR (30),
    last_name VARCHAR (30),
    secret_code VARCHAR (40) UNIQUE,
    type ENUM('reporter', 'target', 'both', 'potential_agent'),
    num_reports INT DEFAULT 0,
    num_mentions INT DEFAULT 0
);

CREATE TABLE IntelReports
(
    id INT PRIMARY KEY AUTO_INCREMENT,
    reporter_id INT NOT NULL, FOREIGN KEY (reporter_id) REFERENCES People(id), 
    target_id INT NOT NULL, FOREIGN KEY (target_id) REFERENCES People(id),
    text TEXT NOT NULL,
    timestamp DATETIME DEFAULT CURRENT_TIMESTAMP()
);