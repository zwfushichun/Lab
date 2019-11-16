SELECT request_session_id   spid,OBJECT_NAME(resource_associated_entity_id) tableName   
from   sys.dm_tran_locks where resource_type='OBJECT'AND request_mode='IX'

--查杀死锁
declare @spid  int 
Set @spid  =49 --锁表进程
declare @sql varchar(1000)
set @sql='kill '+cast(@spid  as varchar)
exec(@sql) 



--快速查看死锁的语句
[sp_who_lock]