# POS System WPF

学校の授業課題として制作した、C# / WPF ベースのPOSレジシステムです。

## 概要
実際のレジ操作をイメージし、商品検索から会計、売上履歴の確認までを行えるデスクトップアプリケーションとして開発しました。

## 主な機能
- バーコード・商品情報を使った商品検索
- カートへの商品追加・数量管理
- 合計金額の計算
- 受取金額からのお釣り計算
- レシート表示
- 商品管理
- 売上データの保存
- 売上履歴の表示・検索
- Today / Month / Total の売上集計
- サイドバーによる画面切り替え

## 使用技術
- C#
- .NET 8
- WPF (XAML)
- Entity Framework Core
- SQL Server LocalDB
- Visual Studio

## 構成
- `PosPage` — レジ・会計画面
- `ProductPage` — 商品管理画面
- `SalesHistoryPage` — 売上履歴画面
- `Models` — 商品・カート・データベース関連モデル

## 工夫した点
レジを初めて操作する人でも分かりやすいよう、画面の見やすさと操作のしやすさを意識しました。また、商品情報と売上データをデータベースで管理し、会計処理だけでなく履歴確認や集計まで一つのアプリで行えるようにしました。

## 注意
ローカルのデータベースファイルやVisual Studioの生成ファイルはリポジトリに含めていません。実行する場合は、各自の環境でSQL Server LocalDBのデータベースを用意し、接続設定を行う必要があります。

---

### English
A desktop POS register system developed with C# and WPF as a school project. It includes product search, cart and checkout processing, change calculation, receipt display, product management, sales history, and sales summaries.
