/**
 * This file contains the exported symbol.
 */
#include "sharedlibrary40.h"
#include <sqlite3.h>
#include <dlog.h>
#include <string.h>
#include <app_alarm.h>
#include <time.h>
static sqlite3 *db;

char result[4096]="";
static int callback(void *counter, int argc, char **argv, char **azColName)
{

    int *localcounter = (int *)counter;
    //char buf[128]="";
    //sprintf(buf," counter=%d, argv0=%s/argv1= %s/argv2= %s/argv3= %s,",*localcounter,argv[0],argv[1],argv[2],argv[3]);
    strncat(result,argv[0],strlen(argv[0]));
    strncat(result,argv[1],strlen(argv[1]));
    strncat(result,argv[2],strlen(argv[2]));
    strncat(result,argv[3],strlen(argv[3]));
    strncat(result,argv[4],strlen(argv[4]));
    strncat(result,",",1);
    (*localcounter)++;
    return 0;
}

void dbopen(){

	sqlite3_shutdown();

    sqlite3_config(SQLITE_CONFIG_URI, 1);

    sqlite3_initialize();
    char *resource = app_get_data_path();
   int siz = strlen(resource) + 10;

   char *path = (char*)malloc(sizeof(char) * siz);
   memset(path, 0, siz);

   strncat(path, resource, siz);
   strncat(path, "test2.db", siz);

   sqlite3_open(path, &db);

   free(resource);
   free(path);

}

void deletefrom(char *con){
	char *ErrMsg;
	char *sql="DELETE FROM Activities";
	sqlite3_exec(db,sql,NULL,0,&ErrMsg);
}


//Activities 테이블을 만듦.
int createtable(){

	   char *ErrMsg;

	  const char *sql = "CREATE TABLE IF NOT EXISTS Activities("
						"TYPE  TEXT  NOT NULL, "
						"START_H  TEXT  NOT NULL, "
						"START_M  TEXT  NOT NULL, "
						"END_H  TEXT  NOT NULL, "
			  	  	  	"END_M  TEXT  NOT NULL);";

	  int ret = sqlite3_exec(db, sql, NULL, 0, &ErrMsg);
	  if(ret!=SQLITE_OK)
		  ret=0;
	  else
		  ret=1;
	  return ret;
}


void insert(char* data1,char* data2,char* data3,char* data4,char *data5){
	 char sqlbuff[1024];
	 snprintf(sqlbuff, 1024,
			  "INSERT INTO Activities VALUES(\'%s\', \'%s\', \'%s\', \'%s\',\'%s\');"
			 ,data1,data2,data3,data4,data5);

	 char *ErrMsg;
	 int ret = sqlite3_exec(db, sqlbuff, NULL, 0, &ErrMsg);

	 if (ret) {
		 sqlite3_free(ErrMsg);
	 }
}

char*
tizensharedlibrary40(void)
{
	 const char *sql = "select * from Activities";
	 int counter = 0;
	 char *ErrMsg;
	 result[0]='\0';

	 int ret = sqlite3_exec(db, sql, callback, &counter, &ErrMsg);
	 if (ret) {
		 dlog_print(DLOG_DEBUG, LOG_TAG, "Error: %s\n", ErrMsg);
		 sqlite3_free(ErrMsg);
	 }
	 return result;
}

int init_alarm(){
	int ret;
	int date_id;
	struct tm date;
	ret=alarm_get_current_time(&date);
	date.tm_sec+=4;

	app_control_h app_control=NULL;
	ret=app_control_create(&app_control);
	ret=app_control_set_operation(app_control,APP_CONTROL_OPERATION_DEFAULT);
	ret=app_control_set_app_id(app_control,"org.example.basicui");
	ret=alarm_schedule_at_date(app_control,&date,0,&date_id);
	if(ret==ALARM_ERROR_NONE)
		return 0;
	else if(ret==ALARM_ERROR_INVALID_PARAMETER)
		return 1;
	else if(ret==ALARM_ERROR_INVALID_DATE)
		return 2;
	else if(ret==ALARM_ERROR_CONNECTION_FAIL)
		return 3;
	else if(ret==ALARM_ERROR_PERMISSION_DENIED)
		return 4;
}
