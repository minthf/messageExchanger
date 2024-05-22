CREATE DATABASE "messageServer";

\connect messageServer;

CREATE TABLE IF NOT EXISTS messages (
    id SERIAL PRIMARY KEY,
    content TEXT NOT NULL,
    date TIMESTAMP NOT NULL,
    serialnumber INT NOT NULL
);