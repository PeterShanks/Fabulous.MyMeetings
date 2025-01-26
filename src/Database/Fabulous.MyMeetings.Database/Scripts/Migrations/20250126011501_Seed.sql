USE [MyMeetings];
GO
BEGIN TRANSACTION

-- Add Administrator
INSERT INTO UserRegistrations.UserRegistrations(
	Id,
	Email,
	Password,
	FirstName,
	LastName,
	Name,
	StatusCode,
	RegisterDate,
	ConfirmedDate
)
VALUES 
(
	'4065630E-4A4C-4F01-9142-0BACF6B8C64D',
	'Peteremadfayek@gmail.com',
	'AQAAAAEAACcQAAAAEI3rRb0hspAhgQA6AVsw4sAUf9dMcBaG4iTBhxBJKcx4ASOeKV27wGy9gHh0rjs7hw==', -- Password = P@ssw0rd
	'Peter',
	'Emad',
	'Peter Emad',
	'Confirmed',
	GETDATE(),
	GETDATE()
)

INSERT INTO Users.Users(
	Id,
	Email,
	Password,
	IsActive,
	FirstName,
	LastName,
	Name
)
VALUES
(
	'4065630E-4A4C-4F01-9142-0BACF6B8C64D',
	'Peteremadfayek@gmail.com',
	'AQAAAAEAACcQAAAAEI3rRb0hspAhgQA6AVsw4sAUf9dMcBaG4iTBhxBJKcx4ASOeKV27wGy9gHh0rjs7hw==', -- Password = P@ssw0rd
	1,
	'Peter',
	'Emad',
	'Peter Emad'
)

INSERT INTO Users.UserRoles(
	UserId,
	RoleCode
)
VALUES
('4065630E-4A4C-4F01-9142-0BACF6B8C64D', 'Administrator')

-- Roles to Permissions

INSERT INTO Users.[Permissions] ([Code], [Name]) VALUES
	-- Meetings
	('GetMeetingGroupProposals', 'GetMeetingGroupProposals'),
	('ProposeMeetingGroup', 'ProposeMeetingGroup'),
	('CreateNewMeeting','CreateNewMeeting'),
	('EditMeeting','EditMeeting'),
	('AddMeetingAttendee','AddMeetingAttendee'),
	('RemoveMeetingAttendee','RemoveMeetingAttendee'),
	('AddNotAttendee','AddNotAttendee'),
	('ChangeNotAttendeeDecision','ChangeNotAttendeeDecision'),
	('SignUpMemberToWaitlist','SignUpMemberToWaitlist'),
	('SignOffMemberFromWaitlist','SignOffMemberFromWaitlist'),
	('SetMeetingHostRole','SetMeetingHostRole'),
	('SetMeetingAttendeeRole','SetMeetingAttendeeRole'),
	('CancelMeeting','CancelMeeting'),
	('GetAllMeetingGroups','GetAllMeetingGroups'),
	('EditMeetingGroupGeneralAttributes','EditMeetingGroupGeneralAttributes'),
	('JoinToGroup','JoinToGroup'),
	('LeaveMeetingGroup','LeaveMeetingGroup'),
	('AddMeetingComment','AddMeetingComment'),
	('EditMeetingComment','EditMeetingComment'),
	('RemoveMeetingComment','RemoveMeetingComment'),
	('AddMeetingCommentReply','AddMeetingCommentReply'),
	('LikeMeetingComment','LikeMeetingComment'),
	('UnlikeMeetingComment','UnlikeMeetingComment'),
	('EnableMeetingCommenting','EnableMeetingCommenting'),
	('DisableMeetingCommenting','DisableMeetingCommenting'),
	('MyMeetingGroupsView','MyMeetingGroupsView'),
	('AllMeetingGroupsView','AllMeetingGroupsView'),
	('SubscriptionView','SubscriptionView'),
	('EmailsView','EmailsView'),
	('MyMeetingsView','MyMeetingsView'),
	('GetAuthenticatedMemberMeetings','GetAuthenticatedMemberMeetings'),
	('GetAuthenticatedMemberMeetingGroups', 'GetAuthenticatedMemberMeetingGroups'),
	('GetMeetingGroupDetails', 'GetMeetingGroupDetails'),
	('GetMeetingDetails', 'GetMeetingDetails'),
	('GetMeetingAttendees', 'GetMeetingAttendees'),
	('MyMeetingsGroupsView', 'MyMeetingsGroupsView'),

	-- Administration
	('AcceptMeetingGroupProposal','AcceptMeetingGroupProposal'),
	('AdministrationsView','AdministrationsView'),

	-- Payments
	('RegisterPayment','RegisterPayment'),
	('BuySubscription','BuySubscription'),
	('RenewSubscription','RenewSubscription'),
	('CreatePriceListItem','CreatePriceListItem'),
	('ActivatePriceListItem','ActivatePriceListItem'),
	('DeactivatePriceListItem','DeactivatePriceListItem'),
	('ChangePriceListItemAttributes','ChangePriceListItemAttributes'),
	('GetAuthenticatedPayerSubscription','GetAuthenticatedPayerSubscription'),
	('GetPriceListItem','GetPriceListItem')

-- Meetings
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'GetMeetingGroupProposals')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'ProposeMeetingGroup')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'CreateNewMeeting')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'EditMeeting')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'AddMeetingAttendee')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'RemoveMeetingAttendee')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'AddNotAttendee')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'ChangeNotAttendeeDecision')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'SignUpMemberToWaitlist')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'SignOffMemberFromWaitlist')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'SetMeetingHostRole')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'SetMeetingAttendeeRole')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'CancelMeeting')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'GetAllMeetingGroups')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'EditMeetingGroupGeneralAttributes')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'JoinToGroup')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'LeaveMeetingGroup')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'AddMeetingComment')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'EditMeetingComment')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'RemoveMeetingComment')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'AddMeetingCommentReply')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'LikeMeetingComment')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'UnlikeMeetingComment')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'GetAuthenticatedMemberMeetingGroups')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'GetMeetingGroupDetails')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'GetMeetingDetails')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'GetMeetingAttendees')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'MyMeetingsGroupsView')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'SubscriptionView')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'EmailsView')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'AllMeetingGroupsView')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'MyMeetingsView')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'GetAuthenticatedMemberMeetings')

-- Administration
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Administrator', 'AcceptMeetingGroupProposal')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Administrator', 'AdministrationsView')

-- Payments
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'RegisterPayment')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'BuySubscription')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'RenewSubscription')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'GetAuthenticatedPayerSubscription')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Member', 'GetPriceListItem')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Administrator', 'CreatePriceListItem')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Administrator', 'ActivatePriceListItem')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Administrator', 'DeactivatePriceListItem')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Administrator', 'ChangePriceListItemAttributes')
INSERT INTO Users.RolePermissions(RoleCode, PermissionCode) VALUES ('Administrator', 'GetPriceListItem')

COMMIT TRANSACTION;

PRINT N'Update complete.';