## ��Ŀ���

**StaticWeb**��һ������ ASP.NET Core 2 + Vue.js ��ǰ��˷����ͨ�ú�̨����ϵͳ��ܡ����ʹ��.NET Core 2 + Entity Framework Core ������UI ����Ŀǰ���еĻ��� Vue.js �� iView����Ŀʵ����ǰ��˵Ķ�̬Ȩ�޹���Ϳ����Լ����� JWT ���û�������֤���ƣ���ǰ��˵Ľ�����������

**StaticWeb**������һ��������ҵ��ϵͳ�������ṩ���ҵ��ϵͳ�ľ������������������ÿһλ.NET �����߶��ܻ���**DncZeus**���ٿ����������������Լ����ܾ߼ѵ�.NET Core ��ҳӦ�ó���(SPA)��

## ֧��StaticWeb(��Start :))

��������StaticWeb��������������ã���ΪDncZeus����ޣ�����ɢ���ø����˻�ð���������

## �ĵ�(Document)
* [github](https://github.com/lampo1024/DncZeus/tree/master/Docs)
* [���� DncZeus](https://codedefault.com/p/getting-started)
* [���������͹���](https://codedefault.com/p/environment-and-developement-tools)
* [������Ŀ&��װ����](https://codedefault.com/p/download-and-restore-dnczeus)
* [DncZeus��Ŀ�ṹ����](https://codedefault.com/p/solution-structure-introduction)
* [�½����ҳ��](https://codedefault.com/p/create-page)

## ��������(Demo)

��������Ա��administrator 
����Ա��admin

���룺111111

��ַ��[https://dnczeus.codedefault.com][7]

�����Բ�ͬ�û�����¼ϵͳ���������鲻ͬ��ɫ�Ĳ�ͬ�˵�Ȩ�ޡ�

**�����Ǹ�����Ŀ���ʽ����ޣ�������ǵ��䣬���Ұ�ϧ���������ʤ�м�������**

*�������ƾ����ַ��[https://github.com/luoyesong102/BaseCore][9]*

## �ʺ���Ⱥ

���� StaticWeb ���ǵ�����.NET �����߶�����ʹ�ã����Ժ����Ŀδ�漰����ܹ��ͷ�װ(�����߼�һĿ��Ȼ)����Ϊ������õ���Ϥ������ StaticWeb������Ҫ�˽⣺

- ASP.NET Core
- Vue.js
- iView

ASP.NET Core ��֪ʶ��ȷ������Կ������˽��������ʵ�ֺ͹����ģ��� Vue.js �������ǰ��ʵ�ֵĻ�ʯ����Ȼ iView ������� Vue.js �� UI ���Ҳ�Ǳ���Ҫ�˽�ģ���Ϊ DncZeus ���ǻ��� [iview-admin][1](iView ��һ����̨����ϵͳʾ����Ŀ)��ʵ�ֵ�ǰ�� UI ������

�����������������֪ʶ������Ϥ�������������ѧϰһЩ������������ DncZeus �����ܡ����� ASP.NET Core �� Vue.js ��������ο���

- [ASP.NET Core �ٷ��ĵ�][2]
- [Vue.js �ٷ��ĵ�][3]

## �����͹���

1. Node.js(ͬʱ��װ npm ǰ�˰�������)
2. Visual Studio 2017(15.8.8 �������ϰ汾)
3. VS Code ��������ǰ�˿�������
4. git ������
5. SQL Server CE ���� SQL Server Express ���� SQL Server 2014 +

## ����ʵ��

- ASP.NET Core 2(.NET Core 2.1.502)
- ASP.NET WebApi Core
- JWT ������֤
- AutoMapper
- Entity Framework Core 2.0
- .NET Core ����ע��
- Swagger UI
- Vue.js(ES6 �﷨)
- iView(���� Vue.js �� UI ���)

## ������Ŀ

### ʹ��Git��������

������ȷ���㱾�ؿ��������Ѱ�װ��git�����ߣ�Ȼ������Ҫ��ű���Ŀ��Ŀ¼��git�����й���**Git Bash Here**�����������������������

```
git clone https://github.com/luoyesong102/BaseCore.git
```

��������Ͱ�StaticWeb��Զ�̴�����ȡ����ı��ؿ������ϡ�


### �ֶ�����

����㲻Ը��ʹ��git����������StaticWeb��Զ�̴��룬��Ҳ������github�йܵ�ַ�ֶ����أ��򿪵�ַ[https://github.com/luoyesong102/BaseCore][4]���ҵ�ҳ���еİ�ť"Clone or download"������ͼʾ��

![�ֶ�����StaticWeb][5]

�ڵ����ĶԻ����е����ť"Download ZIP"���ɿ�ʼ����DncZeus��Դ���룬����ͼ��

![�ֶ�����StaticWebԴ����][6]

## ��װ����

### ǰ����Ŀ

�ڽ�StaticWeb��Դ�������ص�����֮�������ʹ�õ�git�����ߣ�**���Բ���**�˳���ǰ��git�����ߣ������������

```
cd StaticWeb/SAAS.App
```

���뵽StaticWeb��ǰ����ĿĿ¼[SAAS.App](������ֶ����ص�Դ���룬���ڴ�Ŀ¼�������й���)���������������������������ǰ���������Ļ�ԭ������

```
npm install
```

����

```
npm i
```

### �����Ŀ

��Visual Studio�д򿪽������[DncZeus.sln]�����ȸ����Լ��Ŀ�������(SQL Server���ݿ����ͣ���ʾ��Ĭ����SQL Server Localdb)�޸������ļ�`appsettings.json`�е����ݿ������ַ�����ʾ��Ĭ�������ַ���Ϊ��

```
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DncZeus;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
```

�ٴ򿪰��������̨(Package Manager Console)��ִ�����������������ݿ��ṹ��

```
Update-Database -verbose
```

��󣬴���Ŀ��Ŀ¼�еĽű��ļ���[Scripts]��ִ�нű��ļ�[Init_data.sql]�Գ�ʼ��ϵͳ���ݡ�

��ϲ�㣬���������е�׼������������ˡ�

�Ͻ�����StaticWeb��ܰɣ�����


## ����

1. ʹ��Visual Studio�������ߴ�StaticWeb��Ŀ¼�е�VS��������ļ�[StaticWeb.sln](������ϲ���Ļ���ʹ��VS Code������ASP.NET Core�Ŀ���Ҳ�ǿ��Ե�)������SAAS.App��ĿΪĬ����������д���Ŀ��

> ��ʱ��������д򿪵�ַ��http://localhost:54321/swagger ������Բ鿴��DncZeus�Ѿ�ʵ�ֵĺ��API�ӿڷ����ˡ�

2. ���������н��뵽StaticWeb��ǰ����ĿĿ¼[SAAS.App]��������������������ǰ����Ŀ����

```
npm run dev
```

�ɹ����к���Զ���������д򿪵�ַ: http://localhost:9000

## ʹ�ú���Ȩ

StaticWeb��Ŀ��һ����Դ��Ŀ�������ֱ�ӻ��ڱ���Ŀ������չ���߶��ο�����Ҳ�����޸����еĴ��롣



[1]: https://github.com/iview/iview-admin
[2]: https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.2
[3]: https://vuejs.org/
[4]: https://github.com/lampo1024/DncZeus
[5]: https://statics.codedefault.com/uploads/2018/12/1.png
[6]: https://statics.codedefault.com/uploads/2018/12/2.png
[7]: https://dnczeus.codedefault.com
[8]: https://codedefault.com
[9]: https://gitee.com/rector/DncZeus
