/*
<doc>
	<summary>
 		This function returns the address of the person provided.
	</summary>

	<returns>
		Address of the person.
	</returns>
	<remarks>
		This function allows you to request an address for a specific person. It will return
		the first address of that type (multiple address are possible if the individual is in
		multiple families). 
		
		You can provide the address type by specifing 'Home', 'Previous', 
		'Work'. For custom address types provide the AddressTypeId like '19'.

		You can also determine which component of the address you'd like. Values include:
			+ 'Full' - the full address 
			+ 'Street1'
			+ 'Street2'
			+ 'City'
			+ 'State'
			+ 'PostalCode'
			+ 'Country'

	</remarks>
	<code>
		SELECT [dbo].[ufnCrm_GetAddress](3, 'Home', 'Full')
		SELECT [dbo].[ufnCrm_GetAddress](3, 'Home', 'Street1')
		SELECT [dbo].[ufnCrm_GetAddress](3, 'Home', 'Street2')
		SELECT [dbo].[ufnCrm_GetAddress](3, 'Home', 'City')
		SELECT [dbo].[ufnCrm_GetAddress](3, 'Home', 'State')
		SELECT [dbo].[ufnCrm_GetAddress](3, 'Home', 'PostalCode')
		SELECT [dbo].[ufnCrm_GetAddress](3, 'Home', 'Country')
	</code>
</doc>
*/

ALTER FUNCTION [dbo].[ufnCrm_GetAddress](
	@PersonId int, 
	@AddressType varchar(20),
	@AddressComponent varchar(20)) 

RETURNS nvarchar(250) AS

BEGIN
	DECLARE @AddressTypeId int

	-- get address type
	IF (@AddressType = 'Home')
		BEGIN
		SET @AddressTypeId = 19
		END
	ELSE IF (@AddressType = 'Work')
		BEGIN
		SET @AddressTypeId = 20
		END
	ELSE IF (@AddressType = 'Previous')
		BEGIN
		SET @AddressTypeId = 137
		END
	ELSE
		SET @AddressTypeId = CAST(@AddressType AS int)

	-- return address component
	IF (@AddressComponent = 'Street1')
		BEGIN
		RETURN (SELECT [Street1] FROM [Location] WHERE [Id] = (SELECT TOP 1 [LocationId] FROM [GroupLocation] WHERE  [GroupLocationTypeValueId] = @AddressTypeId AND  [GroupId] = (SELECT TOP 1 [GroupId] FROM [GroupMember] gm INNER JOIN [Group] g ON g.[Id] = gm.[GroupId] WHERE [PersonId] = @PersonId AND g.[GroupTypeId] = 10))) 
		END
	ELSE IF (@AddressComponent = 'Street2')
		BEGIN
		RETURN (SELECT [Street2] FROM [Location] WHERE [Id] = (SELECT TOP 1 [LocationId] FROM [GroupLocation] WHERE  [GroupLocationTypeValueId] = @AddressTypeId AND  [GroupId] = (SELECT TOP 1 [GroupId] FROM [GroupMember] gm INNER JOIN [Group] g ON g.[Id] = gm.[GroupId] WHERE [PersonId] = @PersonId AND g.[GroupTypeId] = 10))) 
		END
	ELSE IF (@AddressComponent = 'City')
		BEGIN
		RETURN (SELECT [City] FROM [Location] WHERE [Id] = (SELECT TOP 1 [LocationId] FROM [GroupLocation] WHERE  [GroupLocationTypeValueId] = @AddressTypeId AND  [GroupId] = (SELECT TOP 1 [GroupId] FROM [GroupMember] gm INNER JOIN [Group] g ON g.[Id] = gm.[GroupId] WHERE [PersonId] = @PersonId AND g.[GroupTypeId] = 10))) 
		END
	ELSE IF (@AddressComponent = 'State')
		BEGIN
		RETURN (SELECT [State] FROM [Location] WHERE [Id] = (SELECT TOP 1 [LocationId] FROM [GroupLocation] WHERE  [GroupLocationTypeValueId] = @AddressTypeId AND  [GroupId] = (SELECT TOP 1 [GroupId] FROM [GroupMember] gm INNER JOIN [Group] g ON g.[Id] = gm.[GroupId] WHERE [PersonId] = @PersonId AND g.[GroupTypeId] = 10))) 
		END
	ELSE IF (@AddressComponent = 'PostalCode')
		BEGIN
		RETURN (SELECT [PostalCode] FROM [Location] WHERE [Id] = (SELECT TOP 1 [LocationId] FROM [GroupLocation] WHERE  [GroupLocationTypeValueId] = @AddressTypeId AND  [GroupId] = (SELECT TOP 1 [GroupId] FROM [GroupMember] gm INNER JOIN [Group] g ON g.[Id] = gm.[GroupId] WHERE [PersonId] = @PersonId AND g.[GroupTypeId] = 10))) 
		END
	ELSE IF (@AddressComponent = 'Country')
		BEGIN
		RETURN (SELECT [Country] FROM [Location] WHERE [Id] = (SELECT TOP 1 [LocationId] FROM [GroupLocation] WHERE  [GroupLocationTypeValueId] = @AddressTypeId AND  [GroupId] = (SELECT TOP 1 [GroupId] FROM [GroupMember] gm INNER JOIN [Group] g ON g.[Id] = gm.[GroupId] WHERE [PersonId] = @PersonId AND g.[GroupTypeId] = 10))) 
		END
	ELSE 
		BEGIN
		RETURN (SELECT ISNULL([Street1], '') + ' ' + ISNULL([Street2], '') + ' ' + ISNULL([City], '') + ', ' + ISNULL([State], '') + ' ' + ISNULL([PostalCode], '') FROM [Location] WHERE [Id] = (SELECT TOP 1 [LocationId] FROM [GroupLocation] WHERE  [GroupLocationTypeValueId] = @AddressTypeId AND  [GroupId] = (SELECT TOP 1 [GroupId] FROM [GroupMember] gm INNER JOIN [Group] g ON g.[Id] = gm.[GroupId] WHERE [PersonId] = @PersonId AND g.[GroupTypeId] = 10))) 
		END

	RETURN ''
END