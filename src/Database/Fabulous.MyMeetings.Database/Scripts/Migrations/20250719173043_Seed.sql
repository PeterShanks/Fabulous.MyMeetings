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


INSERT INTO [Meetings].[Countries] VALUES ('AD', 'Andorra')
INSERT INTO [Meetings].[Countries] VALUES ('AE', 'United Arab Emirates')
INSERT INTO [Meetings].[Countries] VALUES ('AF', 'Afghanistan')
INSERT INTO [Meetings].[Countries] VALUES ('AG', 'Antigua and Barbuda')
INSERT INTO [Meetings].[Countries] VALUES ('AI', 'Anguilla')
INSERT INTO [Meetings].[Countries] VALUES ('AL', 'Albania')
INSERT INTO [Meetings].[Countries] VALUES ('AM', 'Armenia')
INSERT INTO [Meetings].[Countries] VALUES ('AN', 'Netherlands Antilles')
INSERT INTO [Meetings].[Countries] VALUES ('AO', 'Angola')
INSERT INTO [Meetings].[Countries] VALUES ('AQ', 'Antarctica')
INSERT INTO [Meetings].[Countries] VALUES ('AR', 'Argentina')
INSERT INTO [Meetings].[Countries] VALUES ('AS', 'American Samoa')
INSERT INTO [Meetings].[Countries] VALUES ('AT', 'Austria')
INSERT INTO [Meetings].[Countries] VALUES ('AU', 'Australia')
INSERT INTO [Meetings].[Countries] VALUES ('AW', 'Aruba')
INSERT INTO [Meetings].[Countries] VALUES ('AX', 'Aland Islands')
INSERT INTO [Meetings].[Countries] VALUES ('AZ', 'Azerbaijan')
INSERT INTO [Meetings].[Countries] VALUES ('BA', 'Bosnia and Herzegovina')
INSERT INTO [Meetings].[Countries] VALUES ('BB', 'Barbados')
INSERT INTO [Meetings].[Countries] VALUES ('BD', 'Bangladesh')
INSERT INTO [Meetings].[Countries] VALUES ('BE', 'Belgium')
INSERT INTO [Meetings].[Countries] VALUES ('BF', 'Burkina Faso')
INSERT INTO [Meetings].[Countries] VALUES ('BG', 'Bulgaria')
INSERT INTO [Meetings].[Countries] VALUES ('BH', 'Bahrain')
INSERT INTO [Meetings].[Countries] VALUES ('BI', 'Burundi')
INSERT INTO [Meetings].[Countries] VALUES ('BJ', 'Benin')
INSERT INTO [Meetings].[Countries] VALUES ('BM', 'Bermuda')
INSERT INTO [Meetings].[Countries] VALUES ('BN', 'Brunei Darussalam')
INSERT INTO [Meetings].[Countries] VALUES ('BO', 'Bolivia')
INSERT INTO [Meetings].[Countries] VALUES ('BR', 'Brazil')
INSERT INTO [Meetings].[Countries] VALUES ('BS', 'Bahamas')
INSERT INTO [Meetings].[Countries] VALUES ('BT', 'Bhutan')
INSERT INTO [Meetings].[Countries] VALUES ('BV', 'Bouvet Island')
INSERT INTO [Meetings].[Countries] VALUES ('BW', 'Botswana')
INSERT INTO [Meetings].[Countries] VALUES ('BY', 'Belarus')
INSERT INTO [Meetings].[Countries] VALUES ('BZ', 'Belize')
INSERT INTO [Meetings].[Countries] VALUES ('CA', 'Canada')
INSERT INTO [Meetings].[Countries] VALUES ('CC', 'Cocos (Keeling) Islands')
INSERT INTO [Meetings].[Countries] VALUES ('CD', 'Congo, The Democratic Republic of the')
INSERT INTO [Meetings].[Countries] VALUES ('CF', 'Central African Republic')
INSERT INTO [Meetings].[Countries] VALUES ('CG', 'Congo')
INSERT INTO [Meetings].[Countries] VALUES ('CH', 'Switzerland')
INSERT INTO [Meetings].[Countries] VALUES ('CI', 'Cote DIvoire')
INSERT INTO [Meetings].[Countries] VALUES ('CK', 'Cook Islands')
INSERT INTO [Meetings].[Countries] VALUES ('CL', 'Chile')
INSERT INTO [Meetings].[Countries] VALUES ('CM', 'Cameroon')
INSERT INTO [Meetings].[Countries] VALUES ('CN', 'China')
INSERT INTO [Meetings].[Countries] VALUES ('CO', 'Colombia')
INSERT INTO [Meetings].[Countries] VALUES ('CR', 'Costa Rica')
INSERT INTO [Meetings].[Countries] VALUES ('CU', 'Cuba')
INSERT INTO [Meetings].[Countries] VALUES ('CV', 'Cape Verde')
INSERT INTO [Meetings].[Countries] VALUES ('CX', 'Christmas Island')
INSERT INTO [Meetings].[Countries] VALUES ('CY', 'Cyprus')
INSERT INTO [Meetings].[Countries] VALUES ('CZ', 'Czech Republic')
INSERT INTO [Meetings].[Countries] VALUES ('DE', 'Germany')
INSERT INTO [Meetings].[Countries] VALUES ('DJ', 'Djibouti')
INSERT INTO [Meetings].[Countries] VALUES ('DK', 'Denmark')
INSERT INTO [Meetings].[Countries] VALUES ('DM', 'Dominica')
INSERT INTO [Meetings].[Countries] VALUES ('DO', 'Dominican Republic')
INSERT INTO [Meetings].[Countries] VALUES ('DZ', 'Algeria')
INSERT INTO [Meetings].[Countries] VALUES ('EC', 'Ecuador')
INSERT INTO [Meetings].[Countries] VALUES ('EE', 'Estonia')
INSERT INTO [Meetings].[Countries] VALUES ('EG', 'Egypt')
INSERT INTO [Meetings].[Countries] VALUES ('EH', 'Western Sahara')
INSERT INTO [Meetings].[Countries] VALUES ('ER', 'Eritrea')
INSERT INTO [Meetings].[Countries] VALUES ('ES', 'Spain')
INSERT INTO [Meetings].[Countries] VALUES ('ET', 'Ethiopia')
INSERT INTO [Meetings].[Countries] VALUES ('FI', 'Finland')
INSERT INTO [Meetings].[Countries] VALUES ('FJ', 'Fiji')
INSERT INTO [Meetings].[Countries] VALUES ('FK', 'Falkland Islands (Malvinas)')
INSERT INTO [Meetings].[Countries] VALUES ('FM', 'Micronesia, Federated States of')
INSERT INTO [Meetings].[Countries] VALUES ('FO', 'Faroe Islands')
INSERT INTO [Meetings].[Countries] VALUES ('FR', 'France')
INSERT INTO [Meetings].[Countries] VALUES ('GA', 'Gabon')
INSERT INTO [Meetings].[Countries] VALUES ('GB', 'United Kingdom')
INSERT INTO [Meetings].[Countries] VALUES ('GD', 'Grenada')
INSERT INTO [Meetings].[Countries] VALUES ('GE', 'Georgia')
INSERT INTO [Meetings].[Countries] VALUES ('GF', 'French Guiana')
INSERT INTO [Meetings].[Countries] VALUES ('GG', 'Guernsey')
INSERT INTO [Meetings].[Countries] VALUES ('GH', 'Ghana')
INSERT INTO [Meetings].[Countries] VALUES ('GI', 'Gibraltar')
INSERT INTO [Meetings].[Countries] VALUES ('GL', 'Greenland')
INSERT INTO [Meetings].[Countries] VALUES ('GM', 'Gambia')
INSERT INTO [Meetings].[Countries] VALUES ('GN', 'Guinea')
INSERT INTO [Meetings].[Countries] VALUES ('GP', 'Guadeloupe')
INSERT INTO [Meetings].[Countries] VALUES ('GQ', 'Equatorial Guinea')
INSERT INTO [Meetings].[Countries] VALUES ('GR', 'Greece')
INSERT INTO [Meetings].[Countries] VALUES ('GS', 'South Georgia and the South Sandwich Islands')
INSERT INTO [Meetings].[Countries] VALUES ('GT', 'Guatemala')
INSERT INTO [Meetings].[Countries] VALUES ('GU', 'Guam')
INSERT INTO [Meetings].[Countries] VALUES ('GW', 'Guinea-Bissau')
INSERT INTO [Meetings].[Countries] VALUES ('GY', 'Guyana')
INSERT INTO [Meetings].[Countries] VALUES ('HK', 'Hong Kong')
INSERT INTO [Meetings].[Countries] VALUES ('HM', 'Heard Island and Mcdonald Islands')
INSERT INTO [Meetings].[Countries] VALUES ('HN', 'Honduras')
INSERT INTO [Meetings].[Countries] VALUES ('HR', 'Croatia')
INSERT INTO [Meetings].[Countries] VALUES ('HT', 'Haiti')
INSERT INTO [Meetings].[Countries] VALUES ('HU', 'Hungary')
INSERT INTO [Meetings].[Countries] VALUES ('ID', 'Indonesia')
INSERT INTO [Meetings].[Countries] VALUES ('IE', 'Ireland')
INSERT INTO [Meetings].[Countries] VALUES ('IL', 'Israel')
INSERT INTO [Meetings].[Countries] VALUES ('IM', 'Isle of Man')
INSERT INTO [Meetings].[Countries] VALUES ('IN', 'India')
INSERT INTO [Meetings].[Countries] VALUES ('IO', 'British Indian Ocean Territory')
INSERT INTO [Meetings].[Countries] VALUES ('IQ', 'Iraq')
INSERT INTO [Meetings].[Countries] VALUES ('IR', 'Iran, Islamic Republic Of')
INSERT INTO [Meetings].[Countries] VALUES ('IS', 'Iceland')
INSERT INTO [Meetings].[Countries] VALUES ('IT', 'Italy')
INSERT INTO [Meetings].[Countries] VALUES ('JE', 'Jersey')
INSERT INTO [Meetings].[Countries] VALUES ('JM', 'Jamaica')
INSERT INTO [Meetings].[Countries] VALUES ('JO', 'Jordan')
INSERT INTO [Meetings].[Countries] VALUES ('JP', 'Japan')
INSERT INTO [Meetings].[Countries] VALUES ('KE', 'Kenya')
INSERT INTO [Meetings].[Countries] VALUES ('KG', 'Kyrgyzstan')
INSERT INTO [Meetings].[Countries] VALUES ('KH', 'Cambodia')
INSERT INTO [Meetings].[Countries] VALUES ('KI', 'Kiribati')
INSERT INTO [Meetings].[Countries] VALUES ('KM', 'Comoros')
INSERT INTO [Meetings].[Countries] VALUES ('KN', 'Saint Kitts and Nevis')
INSERT INTO [Meetings].[Countries] VALUES ('KP', 'Korea, Democratic PeopleS Republic of')
INSERT INTO [Meetings].[Countries] VALUES ('KR', 'Korea, Republic of')
INSERT INTO [Meetings].[Countries] VALUES ('KW', 'Kuwait')
INSERT INTO [Meetings].[Countries] VALUES ('KY', 'Cayman Islands')
INSERT INTO [Meetings].[Countries] VALUES ('KZ', 'Kazakhstan')
INSERT INTO [Meetings].[Countries] VALUES ('LA', 'Lao PeopleS Democratic Republic')
INSERT INTO [Meetings].[Countries] VALUES ('LB', 'Lebanon')
INSERT INTO [Meetings].[Countries] VALUES ('LC', 'Saint Lucia')
INSERT INTO [Meetings].[Countries] VALUES ('LI', 'Liechtenstein')
INSERT INTO [Meetings].[Countries] VALUES ('LK', 'Sri Lanka')
INSERT INTO [Meetings].[Countries] VALUES ('LR', 'Liberia')
INSERT INTO [Meetings].[Countries] VALUES ('LS', 'Lesotho')
INSERT INTO [Meetings].[Countries] VALUES ('LT', 'Lithuania')
INSERT INTO [Meetings].[Countries] VALUES ('LU', 'Luxembourg')
INSERT INTO [Meetings].[Countries] VALUES ('LV', 'Latvia')
INSERT INTO [Meetings].[Countries] VALUES ('LY', 'Libyan Arab Jamahiriya')
INSERT INTO [Meetings].[Countries] VALUES ('MA', 'Morocco')
INSERT INTO [Meetings].[Countries] VALUES ('MC', 'Monaco')
INSERT INTO [Meetings].[Countries] VALUES ('MD', 'Moldova, Republic of')
INSERT INTO [Meetings].[Countries] VALUES ('ME', 'Montenegro')
INSERT INTO [Meetings].[Countries] VALUES ('MG', 'Madagascar')
INSERT INTO [Meetings].[Countries] VALUES ('MH', 'Marshall Islands')
INSERT INTO [Meetings].[Countries] VALUES ('MK', 'Macedonia, The Former Yugoslav Republic of')
INSERT INTO [Meetings].[Countries] VALUES ('ML', 'Mali')
INSERT INTO [Meetings].[Countries] VALUES ('MM', 'Myanmar')
INSERT INTO [Meetings].[Countries] VALUES ('MN', 'Mongolia')
INSERT INTO [Meetings].[Countries] VALUES ('MO', 'Macao')
INSERT INTO [Meetings].[Countries] VALUES ('MP', 'Northern Mariana Islands')
INSERT INTO [Meetings].[Countries] VALUES ('MQ', 'Martinique')
INSERT INTO [Meetings].[Countries] VALUES ('MR', 'Mauritania')
INSERT INTO [Meetings].[Countries] VALUES ('MS', 'Montserrat')
INSERT INTO [Meetings].[Countries] VALUES ('MT', 'Malta')
INSERT INTO [Meetings].[Countries] VALUES ('MU', 'Mauritius')
INSERT INTO [Meetings].[Countries] VALUES ('MV', 'Maldives')
INSERT INTO [Meetings].[Countries] VALUES ('MW', 'Malawi')
INSERT INTO [Meetings].[Countries] VALUES ('MX', 'Mexico')
INSERT INTO [Meetings].[Countries] VALUES ('MY', 'Malaysia')
INSERT INTO [Meetings].[Countries] VALUES ('MZ', 'Mozambique')
INSERT INTO [Meetings].[Countries] VALUES ('NA', 'Namibia')
INSERT INTO [Meetings].[Countries] VALUES ('NC', 'New Caledonia')
INSERT INTO [Meetings].[Countries] VALUES ('NE', 'Niger')
INSERT INTO [Meetings].[Countries] VALUES ('NF', 'Norfolk Island')
INSERT INTO [Meetings].[Countries] VALUES ('NG', 'Nigeria')
INSERT INTO [Meetings].[Countries] VALUES ('NI', 'Nicaragua')
INSERT INTO [Meetings].[Countries] VALUES ('NL', 'Netherlands')
INSERT INTO [Meetings].[Countries] VALUES ('NO', 'Norway')
INSERT INTO [Meetings].[Countries] VALUES ('NP', 'Nepal')
INSERT INTO [Meetings].[Countries] VALUES ('NR', 'Nauru')
INSERT INTO [Meetings].[Countries] VALUES ('NU', 'Niue')
INSERT INTO [Meetings].[Countries] VALUES ('NZ', 'New Zealand')
INSERT INTO [Meetings].[Countries] VALUES ('OM', 'Oman')
INSERT INTO [Meetings].[Countries] VALUES ('PA', 'Panama')
INSERT INTO [Meetings].[Countries] VALUES ('PE', 'Peru')
INSERT INTO [Meetings].[Countries] VALUES ('PF', 'French Polynesia')
INSERT INTO [Meetings].[Countries] VALUES ('PG', 'Papua New Guinea')
INSERT INTO [Meetings].[Countries] VALUES ('PH', 'Philippines')
INSERT INTO [Meetings].[Countries] VALUES ('PK', 'Pakistan')
INSERT INTO [Meetings].[Countries] VALUES ('PL', 'Poland')
INSERT INTO [Meetings].[Countries] VALUES ('PM', 'Saint Pierre and Miquelon')
INSERT INTO [Meetings].[Countries] VALUES ('PN', 'Pitcairn')
INSERT INTO [Meetings].[Countries] VALUES ('PR', 'Puerto Rico')
INSERT INTO [Meetings].[Countries] VALUES ('PS', 'Palestinian Territory, Occupied')
INSERT INTO [Meetings].[Countries] VALUES ('PT', 'Portugal')
INSERT INTO [Meetings].[Countries] VALUES ('PW', 'Palau')
INSERT INTO [Meetings].[Countries] VALUES ('PY', 'Paraguay')
INSERT INTO [Meetings].[Countries] VALUES ('QA', 'Qatar')
INSERT INTO [Meetings].[Countries] VALUES ('RE', 'Reunion')
INSERT INTO [Meetings].[Countries] VALUES ('RO', 'Romania')
INSERT INTO [Meetings].[Countries] VALUES ('RS', 'Serbia')
INSERT INTO [Meetings].[Countries] VALUES ('RU', 'Russian Federation')
INSERT INTO [Meetings].[Countries] VALUES ('RW', 'RWANDA')
INSERT INTO [Meetings].[Countries] VALUES ('SA', 'Saudi Arabia')
INSERT INTO [Meetings].[Countries] VALUES ('SB', 'Solomon Islands')
INSERT INTO [Meetings].[Countries] VALUES ('SC', 'Seychelles')
INSERT INTO [Meetings].[Countries] VALUES ('SD', 'Sudan')
INSERT INTO [Meetings].[Countries] VALUES ('SE', 'Sweden')
INSERT INTO [Meetings].[Countries] VALUES ('SG', 'Singapore')
INSERT INTO [Meetings].[Countries] VALUES ('SH', 'Saint Helena')
INSERT INTO [Meetings].[Countries] VALUES ('SI', 'Slovenia')
INSERT INTO [Meetings].[Countries] VALUES ('SJ', 'Svalbard and Jan Mayen')
INSERT INTO [Meetings].[Countries] VALUES ('SK', 'Slovakia')
INSERT INTO [Meetings].[Countries] VALUES ('SL', 'Sierra Leone')
INSERT INTO [Meetings].[Countries] VALUES ('SM', 'San Marino')
INSERT INTO [Meetings].[Countries] VALUES ('SN', 'Senegal')
INSERT INTO [Meetings].[Countries] VALUES ('SO', 'Somalia')
INSERT INTO [Meetings].[Countries] VALUES ('SR', 'Suriname')
INSERT INTO [Meetings].[Countries] VALUES ('ST', 'Sao Tome and Principe')
INSERT INTO [Meetings].[Countries] VALUES ('SV', 'El Salvador')
INSERT INTO [Meetings].[Countries] VALUES ('SY', 'Syrian Arab Republic')
INSERT INTO [Meetings].[Countries] VALUES ('SZ', 'Swaziland')
INSERT INTO [Meetings].[Countries] VALUES ('TC', 'Turks and Caicos Islands')
INSERT INTO [Meetings].[Countries] VALUES ('TD', 'Chad')
INSERT INTO [Meetings].[Countries] VALUES ('TF', 'French Southern Territories')
INSERT INTO [Meetings].[Countries] VALUES ('TG', 'Togo')
INSERT INTO [Meetings].[Countries] VALUES ('TH', 'Thailand')
INSERT INTO [Meetings].[Countries] VALUES ('TJ', 'Tajikistan')
INSERT INTO [Meetings].[Countries] VALUES ('TK', 'Tokelau')
INSERT INTO [Meetings].[Countries] VALUES ('TL', 'Timor-Leste')
INSERT INTO [Meetings].[Countries] VALUES ('TM', 'Turkmenistan')
INSERT INTO [Meetings].[Countries] VALUES ('TN', 'Tunisia')
INSERT INTO [Meetings].[Countries] VALUES ('TO', 'Tonga')
INSERT INTO [Meetings].[Countries] VALUES ('TR', 'Turkey')
INSERT INTO [Meetings].[Countries] VALUES ('TT', 'Trinidad and Tobago')
INSERT INTO [Meetings].[Countries] VALUES ('TV', 'Tuvalu')
INSERT INTO [Meetings].[Countries] VALUES ('TW', 'Taiwan, Province of China')
INSERT INTO [Meetings].[Countries] VALUES ('TZ', 'Tanzania, United Republic of')
INSERT INTO [Meetings].[Countries] VALUES ('UA', 'Ukraine')
INSERT INTO [Meetings].[Countries] VALUES ('UG', 'Uganda')
INSERT INTO [Meetings].[Countries] VALUES ('UM', 'United States Minor Outlying Islands')
INSERT INTO [Meetings].[Countries] VALUES ('US', 'United States')
INSERT INTO [Meetings].[Countries] VALUES ('UY', 'Uruguay')
INSERT INTO [Meetings].[Countries] VALUES ('UZ', 'Uzbekistan')
INSERT INTO [Meetings].[Countries] VALUES ('VA', 'Holy See (Vatican City State)')
INSERT INTO [Meetings].[Countries] VALUES ('VC', 'Saint Vincent and the Grenadines')
INSERT INTO [Meetings].[Countries] VALUES ('VE', 'Venezuela')
INSERT INTO [Meetings].[Countries] VALUES ('VG', 'Virgin Islands, British')
INSERT INTO [Meetings].[Countries] VALUES ('VI', 'Virgin Islands, U.S.')
INSERT INTO [Meetings].[Countries] VALUES ('VN', 'Viet Nam')
INSERT INTO [Meetings].[Countries] VALUES ('VU', 'Vanuatu')
INSERT INTO [Meetings].[Countries] VALUES ('WF', 'Wallis and Futuna')
INSERT INTO [Meetings].[Countries] VALUES ('WS', 'Samoa')
INSERT INTO [Meetings].[Countries] VALUES ('YE', 'Yemen')
INSERT INTO [Meetings].[Countries] VALUES ('YT', 'Mayotte')
INSERT INTO [Meetings].[Countries] VALUES ('ZA', 'South Africa')
INSERT INTO [Meetings].[Countries] VALUES ('ZM', 'Zambia')
INSERT INTO [Meetings].[Countries] VALUES ('ZW', 'Zimbabwe')

COMMIT TRANSACTION;

PRINT N'Update complete.';