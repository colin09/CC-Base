


# 数据初始化
    EventType :
        赛事分类跟节点 name=赛事分类，parentId = 0;
        INSERT INTO `EventType` (`CreateTime`,`UpdateTime`,`IsDeleted`,`ParentId`,`Name`,`Level`,`IsShow`,`Icon`,`SortNo`)
        VALUES ('2018-12-16 16:45:31', '2018-12-16 16:45:31', '0', '0', '赛事分类', '1', '0', '', '1');
    
    UserInfo:
        初始化开发角色账户，用于创建后台管理账户，  
        INSERT INTO `UserInfo` (`CreateTime`,`UpdateTime`,`IsDeleted`,`UserName`,`Password`,`TrueName`,`Department`,`State`,`AuthType`,`MobileNo`,`Email`,`Gender`)
        VALUES ('2018-12-12 12:12:12', '2018-12-12 12:12:12', '0', 'dev', 'e10adc3949ba59abbe56e057f20f883e', 'developer', 'developer', '1', '1', '18900001111', '1@q.com', '0');


    根目录下创建文件夹 SourcesFile





字符问题处理:

ALTER DATABASE hui CHARACTER utf8 utf8;

SELECT TABLE_NAME,CONCAT('ALTER TABLE  ',TABLE_NAME,' DEFAULT CHARACTER SET ',a.DEFAULT_CHARACTER_SET_NAME,' COLLATE ',a.DEFAULT_COLLATION_NAME,';') executeSQL 
FROM information_schema.SCHEMATA a,information_schema.TABLES b
WHERE a.SCHEMA_NAME=b.TABLE_SCHEMA
AND a.DEFAULT_COLLATION_NAME!=b.TABLE_COLLATION
AND b.TABLE_SCHEMA='hui' ;

SELECT b.table_name,b.column_name,b.character_set_name,b.collation_name
,CONCAT('ALTER TABLE ',b.table_name,' MODIFY ',b.column_name,' ',b.DATA_TYPE,'(',b.CHARACTER_MAXIMUM_LENGTH,') ',CASE WHEN b.COLUMN_DEFAULT IS NULL THEN ''  ELSE CONCAT('DEFAULT \'',b.COLUMN_DEFAULT,'\'') END,' COMMENT \'',b.COLUMN_COMMENT,'\';') executeSQL
FROM information_schema.TABLES a,information_schema.COLUMNS b 
WHERE  b.character_set_name IS NOT NULL and a.TABLE_SCHEMA=b.TABLE_SCHEMA AND a.TABLE_NAME=b.TABLE_NAME
AND a.TABLE_COLLATION!=b.COLLATION_NAME
AND a.TABLE_SCHEMA='hui';
