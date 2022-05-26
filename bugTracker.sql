CREATE TABLE `UserProfile` (
  `Id` int PRIMARY KEY AUTO_INCREMENT,
  `Name` varcharacter NOT NULL,
  `UserType` varcharacter,
  `FirbaseUserId` varcharacter NOT NULL
);

CREATE TABLE `Bug` (
  `Id` int PRIMARY KEY AUTO_INCREMENT,
  `Description` varcharacter,
  `Line` int,
  `Sovled` boolean,
  `projectId` int,
  `BugTypeId` int
);

CREATE TABLE `ProjectList` (
  `id` int AUTO_INCREMENT,
  `UserProfileId` int AUTO_INCREMENT,
  `ProgrammingLanguage` varcharacter,
  `ProjectName` varcharacter,
  PRIMARY KEY (`id`, `UserProfileId`)
);

CREATE TABLE `BugType` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `BugType` varcharacter
);

ALTER TABLE `Bug` ADD FOREIGN KEY (`projectId`) REFERENCES `ProjectList` (`id`);

ALTER TABLE `ProjectList` ADD FOREIGN KEY (`UserProfileId`) REFERENCES `UserProfile` (`Id`);

ALTER TABLE `Bug` ADD FOREIGN KEY (`BugTypeId`) REFERENCES `BugType` (`id`);
