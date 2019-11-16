Æ´½Ó×Ö·û´® FOR XML PATH

SELECT A.*,(SELECT(STUFF((SELECT ',' + AirPortCode FROM dbo.AirPort
                            WHERE Id IN(SELECT TransEndAirPortId FROM AirTransPortGroup WHERE AirTransShipingPriceId=A.Id)
                            FOR XML PATH('')),1,1,''))) AS TransEndAirPortDisplay 
FROM AirTransShipingPrice AS A 