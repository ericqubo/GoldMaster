/*
       Licensed to the Apache Software Foundation (ASF) under one
       or more contributor license agreements.  See the NOTICE file
       distributed with this work for additional information
       regarding copyright ownership.  The ASF licenses this file
       to you under the Apache License, Version 2.0 (the
       "License"); you may not use this file except in compliance
       with the License.  You may obtain a copy of the License at

         http://www.apache.org/licenses/LICENSE-2.0

       Unless required by applicable law or agreed to in writing,
       software distributed under the License is distributed on an
       "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
       KIND, either express or implied.  See the License for the
       specific language governing permissions and limitations
       under the License.
 */

package com.phonegap.goldmaster;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;

import android.app.AlertDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.os.Handler;
import android.os.Message;
import android.util.Log;

import org.apache.cordova.*;
import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;
import org.json.JSONObject;


public class GoldMaster extends CordovaActivity 
{
	private static final String TAG = "Update";
	public ProgressDialog pBar;
	private Handler handler = new Handler();

	private int ver = 0;
	private String mversion = "";
	private String mcontent = "";
	private String versionName = "";
	private String content = "";
	private String updateurl = "";
	private String apkname = "";
	private String mversionname = "";
    @Override
    public void onCreate(Bundle savedInstanceState)
    {
    	super.setIntegerProperty("loadUrlTimeoutValue", 60000);
		super.setIntegerProperty("splashscreen", R.drawable.splash);
        super.onCreate(savedInstanceState);
        super.init();
        // Set by <content src="index.html" /> in config.xml
        super.loadUrl(Config.getStartUrl());
        //super.loadUrl("file:///android_asset/www/index.html");
        new Thread(new Runnable() {

			@Override
			public void run() {
				if (getServerVerCode()) {
					int vercode = UpdateConfig.getVerCode(GoldMaster.this);
					if (ver > vercode) {
						Message message = new Message();
						message.what = 1;
						mHandler.sendMessage(message);
						//doNewVersionUpdate();
					} else {
						//notNewVersionShow();
					}
				}

			}
		}).start();

    }

	private Handler mHandler = new Handler() {

		public void handleMessage(Message msg) {
			switch (msg.what) {
			case 1:
				doNewVersionUpdate();
				break;
			}
		};
	};

	private boolean getServerVerCode() {
		try {
			String verjson = NetworkTool.getContent(UpdateConfig.UPDATE_SERVER);
			try {
				JSONObject obj = new JSONObject(verjson);
				ver = obj.getInt("v");
				mversion = obj.getString("mv");
				//mcontent = obj.getString("mc");
				versionName = obj.getString("vn");
				content = obj.getString("c");
				updateurl = obj.getString("u");
				apkname = obj.getString("n");
				//mversionname = obj.getString("mn");

			} catch (Exception e) {
				//newVerCode = -1;
				//newVerName = "";
				return false;
			}

		} catch (Exception e) {
			Log.e(TAG, e.getMessage());
			return false;
		}
		return true;
	}

	private void notNewVersionShow() {
		int verCode = UpdateConfig.getVerCode(this);
		String verName = UpdateConfig.getVerName(this);
		StringBuffer sb = new StringBuffer();
		sb.append("当前版本:");
		sb.append(verName);
		sb.append(" Code:");
		sb.append(verCode);
		sb.append(",\n已是最新版,无需更新!");
		Dialog dialog = new AlertDialog.Builder(GoldMaster.this)
				.setTitle("软件更新").setMessage(sb.toString())// 设置内容
				.setPositiveButton("确定",// 设置确定按钮
						new DialogInterface.OnClickListener() {

							@Override
							public void onClick(DialogInterface dialog,
									int which) {
								finish();
							}

						}).create();// 创建
		// 显示对话框
		dialog.show();
	}

	private void doNewVersionUpdate() {
		StringBuffer sb = new StringBuffer();
		sb.append(content);
		//sb.append("当前版本:");
		//sb.append(verName);
		//sb.append(", 发现新版本:");
		//sb.append(versionName);
		//sb.append(", 是否更新?");
		Dialog dialog = new AlertDialog.Builder(GoldMaster.this)
				.setTitle("软件更新")
				.setMessage(sb.toString())
				// 设置内容
				.setPositiveButton("更新",// 设置确定按钮
						new DialogInterface.OnClickListener() {

							@Override
							public void onClick(DialogInterface dialog,
									int which) {
								pBar = new ProgressDialog(GoldMaster.this);
								pBar.setTitle("正在下载");
								pBar.setMessage("请稍候...");
								pBar.setProgressStyle(ProgressDialog.STYLE_SPINNER);
								downFile(updateurl);
							}

						})
				.setNegativeButton("暂不更新",
						new DialogInterface.OnClickListener() {
							public void onClick(DialogInterface dialog,
									int whichButton) {
								// 点击"取消"按钮之后退出程序
								//finish();
							}
						}).create();// 创建
		// 显示对话框
		dialog.show();
	}

	void downFile(final String url) {
		pBar.show();
		new Thread() {
			public void run() {
				HttpClient client = new DefaultHttpClient();
				HttpGet get = new HttpGet(url);
				HttpResponse response;
				try {
					response = client.execute(get);
					HttpEntity entity = response.getEntity();
					long length = entity.getContentLength();
					InputStream is = entity.getContent();
					FileOutputStream fileOutputStream = null;
					if (is != null) {

						File file = new File(
								Environment.getExternalStorageDirectory(),
								apkname);
						fileOutputStream = new FileOutputStream(file);

						byte[] buf = new byte[1024];
						int ch = -1;
						int count = 0;
						while ((ch = is.read(buf)) != -1) {
							fileOutputStream.write(buf, 0, ch);
							count += ch;
							if (length > 0) {
							}
						}

					}
					fileOutputStream.flush();
					if (fileOutputStream != null) {
						fileOutputStream.close();
					}
					down();
				} catch (ClientProtocolException e) {
					e.printStackTrace();
				} catch (IOException e) {
					e.printStackTrace();
				}
			}

		}.start();
	}

	void down() {
		handler.post(new Runnable() {
			public void run() {
				pBar.cancel();
				update();
			}
		});

	}

	void update() {

		Intent intent = new Intent(Intent.ACTION_VIEW);
		intent.setDataAndType(Uri.fromFile(new File(Environment
				.getExternalStorageDirectory(), apkname)),
				"application/vnd.android.package-archive");
		startActivity(intent);
	}

}

