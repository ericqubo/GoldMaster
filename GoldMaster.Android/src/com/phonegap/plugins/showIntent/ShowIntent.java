/**
 * 
 */
/**
 * @author Administrator
 *
 */
package com.phonegap.plugins.showIntent;

import org.json.JSONArray;
import org.json.JSONObject;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.AlertDialog.Builder;
import android.content.ComponentName;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.view.WindowManager;
import android.view.inputmethod.InputMethodManager;

import org.apache.cordova.CordovaPlugin;
import org.apache.cordova.CallbackContext;
import org.apache.cordova.PluginResult;

public class ShowIntent extends CordovaPlugin {

    public ShowIntent() {
    	
    }
    @Override
	public boolean execute(String action, JSONArray args, CallbackContext callbackContext) {
       final Activity activity=this.cordova.getActivity();
       JSONObject obj = args.optJSONObject(0);
       if (obj != null) {
    	  final String page = obj.optString("page");
    	if (action.equals("ShowSystemPage")) {
    		String title="";
    		String content="";
    		if(page.equals("WirelessSettings")){
    			title="没有可用的网络";
    			content="是否对网络进行设置？";
    		}else if(page.equals("LocationSettings"))
    		{
    			title="定位失败";
    			content="是否对定位进行设置？";
    		}
             Builder b = new AlertDialog.Builder(this.cordova.getActivity()).setTitle(title)
                     .setMessage(content);
             b.setNegativeButton("是", new DialogInterface.OnClickListener() {
                 public void onClick(DialogInterface dialog, int whichButton) {
                     
                     Intent intent;
                     if(page.equals("WirelessSettings")){
	                     if(android.os.Build.VERSION.SDK_INT > 10 ){
	                         //3.0以上打开设置界面
	                    	 intent=new Intent(android.provider.Settings.ACTION_SETTINGS);
		                 }else
		                 {
		                	 intent=new Intent(android.provider.Settings.ACTION_WIRELESS_SETTINGS);
		                 }
                     }else if(page.equals("LocationSettings"))
                     {
                    	 intent=new Intent(android.provider.Settings.ACTION_LOCATION_SOURCE_SETTINGS);
                     }
                     else
                     {
                    	 intent=new Intent(android.provider.Settings.ACTION_SETTINGS);
                     }
                     activity.startActivityForResult(intent,0);  // 如果在设置完成后需要再次进行操作，可以重写操作代码，在这里不再重写
                     //activity.startActivity(mIntent);
                 }

             }).setNeutralButton("否", new DialogInterface.OnClickListener() {
                 public void onClick(DialogInterface dialog, int whichButton) {
                     dialog.cancel();
                 }
             }).show();
             callbackContext.success("sucess");
             return true;
    	}
    	else
    	{
    		callbackContext.error("指定的方法不存在");
    		return false;
    	}
       }
       else
       {
    	   callbackContext.error("参数不正确");
    	   return false;
       }
         
    }
}